import React from "react";
import CreateProjectForm from "../components/Project/CreateProjectForm";
import MyProjects from "../components/Project/MyProjects";

export default function MyProjectPage() {
  return (
    <div className="p-6 space-y-6">
      <h1 className="text-3xl font-bold">My Project Page</h1>
      <CreateProjectForm />
      <MyProjects />
    </div>
  );
}
