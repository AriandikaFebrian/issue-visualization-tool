import React, { useEffect, useState } from 'react';
import Tree from 'rc-tree';
import 'rc-tree/assets/index.css';
import axios from 'axios';
import { AiFillFolder, AiFillFile } from 'react-icons/ai';

interface TreeNode {
  name: string;
  path: string;
  type: 'file' | 'folder';
  children?: TreeNode[];
}

interface FileTreeViewerProps {
  projectId: string;
}

const FileTreeViewer: React.FC<FileTreeViewerProps> = ({ projectId }) => {
  const [treeData, setTreeData] = useState<any[]>([]);
  const [selectedFileContent, setSelectedFileContent] = useState<string>('');
  const [error, setError] = useState<string>('');

  // Convert backend TreeNode to rc-tree format
// di convertToRcTree:
const convertToRcTree = (node: TreeNode): any => ({
  title: (
    <span>
      {node.type === 'folder' ? <AiFillFolder style={{ marginRight: 4 }} /> : <AiFillFile style={{ marginRight: 4 }} />}
      {node.name}
    </span>
  ),
  key: node.path,
  isLeaf: node.type === 'file',
  children: node.children?.map(convertToRcTree),
  data: {
    path: node.path,
    type: node.type,
  },
});

  // Fetch tree structure
  useEffect(() => {
    const fetchTree = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('Token not found. Please login.');
        return;
      }

      try {
        const response = await axios.get(`/api/Project/${projectId}/source-tree`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        const rootNode: TreeNode = response.data;
        setTreeData([convertToRcTree(rootNode)]);
      } catch (err) {
        console.error('Error fetching source tree:', err);
        setError('Failed to load source tree.');
      }
    };

    if (projectId) fetchTree();
    else setError('No projectId provided.');
  }, [projectId]);

const fetchFileContent = async (path: string) => {
  const token = localStorage.getItem('token');
  if (!token) {
    setError('Token not found. Please login.');
    return;
  }

  try {
    console.log('Fetching file content for:', path); // Tambahkan log
    const response = await axios.get(
      `/api/Project/${projectId}/source-file`,
      {
        params: { path },
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    console.log('File content fetched:', response.data); // Tambahkan log
    setSelectedFileContent(response.data.content);
  } catch (err: any) {
    console.error('Error fetching file content:', err); // Detail error
    if (err.response) {
      setSelectedFileContent(
        `Error ${err.response.status}: ${err.response.statusText}\n${JSON.stringify(err.response.data)}`
      );
    } else {
      setSelectedFileContent('Failed to load file content.');
    }
  }
};




  return (
    <div style={{ display: 'flex', gap: '2rem', padding: '2rem' }}>
      <div style={{ flex: 1 }}>
        <h3>Source Tree</h3>
        {error && <p style={{ color: 'red' }}>{error}</p>}
       

<Tree
  treeData={treeData}
  defaultExpandAll
  onSelect={(selectedKeys, { node }) => {
    if (node.data?.type === 'file') {
      const path = node.data.path;
      console.log('Selected file path:', path);
      fetchFileContent(path);
    }
  }}
/>


      </div>
      <div style={{ flex: 2 }}>
        <h3>File Preview</h3>
        <pre
          style={{
            background: '#f5f5f5',
            padding: '1rem',
            borderRadius: 8,
            color: '#333',
            whiteSpace: 'pre-wrap',
            overflow: 'auto',
            maxHeight: '600px',
          }}
        >
          {selectedFileContent || 'Klik salah satu file untuk melihat isinya'}
        </pre>
      </div>
    </div>
  );
};

export default FileTreeViewer;
