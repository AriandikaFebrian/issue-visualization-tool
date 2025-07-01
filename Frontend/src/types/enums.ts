export const UserRole = [
  "Admin",
  "Manager",
  "Developer",
  "QA",
  "Client",
  "Guest"
] as const;

export type UserRole = (typeof UserRole)[number];

export const PositionType = [
  "Intern",
  "JuniorDeveloper",
  "MidDeveloper",
  "SeniorDeveloper",
  "TechLead",
  "ProjectManager",
  "QATester",
  "UXDesigner",
  "ProductOwner",
  "BusinessAnalyst",
  "DevOpsEngineer",
  "SystemAdministrator",
  "SupportEngineer",
  "Other"
] as const;

export type PositionType = (typeof PositionType)[number];

export const DepartmentType = [
  "Engineering",
  "QualityAssurance",
  "Product",
  "Design",
  "HumanResources",
  "Marketing",
  "Finance",
  "Operations",
  "ITSupport",
  "Legal",
  "Other"
] as const;

export type DepartmentType = (typeof DepartmentType)[number];

export const ProjectStatus = [
  "Planning",
  "InProgress",
  "OnHold",
  "Completed",
  "Archived",
] as const;

export type ProjectStatus = (typeof ProjectStatus)[number];

export const ProjectVisibility = [
  "Private",
  "Public",
  "Internal",
] as const;

export type ProjectVisibility = (typeof ProjectVisibility)[number];



