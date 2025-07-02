import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  Box,
  CircularProgress,
  Typography,
  Paper,
  Button,
  CssBaseline,
  createTheme,
  ThemeProvider,
  IconButton,
  Tooltip,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import Tree from "rc-tree";

import "rc-tree/assets/index.css";
import "@fontsource/roboto-mono";

import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import DownloadIcon from "@mui/icons-material/Download";

import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { oneDark } from "react-syntax-highlighter/dist/esm/styles/prism";

interface FileNode {
  name: string;
  path: string;
  type: "file" | "folder";
  children?: FileNode[];
}

interface TreeNode {
  title: string | React.ReactNode;
  key: string;
  isLeaf?: boolean;
  children?: TreeNode[];
}

const darkTheme = createTheme({
  palette: {
    mode: "dark",
    background: {
      default: "#0d1117",
      paper: "#161b22",
    },
    text: {
      primary: "#c9d1d9",
      secondary: "#8b949e",
    },
  },
  typography: {
    fontFamily: ["Roboto Mono", "monospace"].join(","),
  },
});

const SourceCodeViewer: React.FC = () => {
  const { projectCode } = useParams<{ projectCode: string }>();
  const [treeData, setTreeData] = useState<TreeNode[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedFileContent, setSelectedFileContent] = useState<string | null>(null);
  const [selectedFilePath, setSelectedFilePath] = useState<string | null>(null);
  const navigate = useNavigate();

  const token = localStorage.getItem("token");

  useEffect(() => {
    const fetchTree = async () => {
      try {
        const res = await axios.get(
          `https://localhost:5001/api/Project/${projectCode}/source-tree`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!res.data) return;

        const converted = convertToTreeNodes(res.data);
        setTreeData([converted]);
      } catch (err) {
        console.error("Gagal mengambil source tree", err);
      } finally {
        setLoading(false);
      }
    };

    if (projectCode && token) {
      fetchTree();
    } else {
      setLoading(false);
    }
  }, [projectCode, token]);

  const renderTreeTitle = (node: TreeNode) => (
    <span>
      {node.isLeaf ? "📄 " : "📁 "}
      {node.title}
    </span>
  );

  const convertToTreeNodes = (node: FileNode): TreeNode => ({
    title: renderTreeTitle({
      title: node.name,
      key: node.path,
      isLeaf: node.type === "file",
    }),
    key: node.path,
    isLeaf: node.type === "file",
    children: node.children?.map(convertToTreeNodes),
  });

  const handleSelect = async (keys: React.Key[], info: any) => {
    const node: TreeNode = info.node;
    if (node.isLeaf && node.key && projectCode) {
      try {
        const res = await axios.get(
          `https://localhost:5001/api/Project/${projectCode}/source-file`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
            params: {
              path: node.key,
            },
          }
        );
        setSelectedFilePath(node.key);
        setSelectedFileContent(res.data.content);
      } catch (err) {
        console.error("Gagal mengambil isi file", err);
        setSelectedFileContent("// Gagal memuat file.");
      }
    }
  };

  const copyToClipboard = () => {
    if (selectedFileContent) {
      navigator.clipboard.writeText(selectedFileContent);
    }
  };

  const downloadFile = () => {
    if (selectedFileContent && selectedFilePath) {
      const element = document.createElement("a");
      const file = new Blob([selectedFileContent], { type: "text/plain" });
      element.href = URL.createObjectURL(file);
      element.download = selectedFilePath.split("/").pop() ?? "file.txt";
      document.body.appendChild(element);
      element.click();
    }
  };

  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <Box p={3} sx={{ backgroundColor: "#0d1117", minHeight: "100vh", color: "#c9d1d9" }}>
        <Button
          variant="outlined"
          onClick={() => navigate(-1)}
          sx={{
            borderColor: "#30363d",
            color: "#c9d1d9",
            mb: 2,
            "&:hover": {
              borderColor: "#8b949e",
            },
          }}
        >
          ← Kembali
        </Button>

        <Typography variant="h5" fontWeight="bold" gutterBottom>
          Source Code Tree
        </Typography>

        {loading ? (
          <Box mt={4} textAlign="center">
            <CircularProgress sx={{ color: "#58a6ff" }} />
            <Typography mt={2}>Memuat struktur source code...</Typography>
          </Box>
        ) : (
          <Box display="flex" gap={3}>
            <Paper
              sx={{
                backgroundColor: "#161b22",
                p: 2,
                width: "35%",
                maxHeight: "70vh",
                overflowY: "auto",
              }}
            >
              <Tree
                treeData={treeData}
                defaultExpandAll
                showIcon={false}
                height={500}
                onSelect={handleSelect}
              />
            </Paper>

            <Paper
              sx={{
                backgroundColor: "#161b22",
                p: 2,
                flex: 1,
                overflowX: "auto",
              }}
            >
              {selectedFileContent ? (
                <>
                  <Box display="flex" justifyContent="space-between" alignItems="center">
                    <Typography variant="h6" gutterBottom sx={{ color: "#58a6ff" }}>
                      📄 {selectedFilePath}
                    </Typography>
                    <Box>
                      <Tooltip title="Copy to clipboard">
                        <IconButton onClick={copyToClipboard} sx={{ color: "#c9d1d9" }}>
                          <ContentCopyIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Download file">
                        <IconButton onClick={downloadFile} sx={{ color: "#c9d1d9" }}>
                          <DownloadIcon fontSize="small" />
                        </IconButton>
                      </Tooltip>
                    </Box>
                  </Box>

                  <SyntaxHighlighter
                    language="tsx"
                    style={oneDark}
                    customStyle={{
                      background: "#161b22",
                      padding: "1rem",
                      borderRadius: "6px",
                      fontSize: "14px",
                    }}
                  >
                    {selectedFileContent}
                  </SyntaxHighlighter>
                </>
              ) : (
                <Typography color="textSecondary">
                  Pilih file dari struktur di kiri untuk melihat isinya.
                </Typography>
              )}
            </Paper>
          </Box>
        )}
      </Box>
    </ThemeProvider>
  );
};

export default SourceCodeViewer;
