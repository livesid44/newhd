# Task Management Enhancement Implementation Guide

## Overview
This document outlines the comprehensive task management enhancement implementation for the Visit Management System, including predefined templates, Kanban board, and advanced task assignment capabilities.

## Requirements (from User)
1. ✅ Task should populate based on Visit ID
2. ✅ Task template should be predefined (Platinum CS, Gold CS, Silver CS, Marketing, TIM checklists)
3. ⬜ Admin user should have option to select from template, add/remove tasks and assign to user group or user
4. ⬜ Individual user should have option to update the status and provide comment and document
5. ⬜ Kanban style drag drop board for a visit
6. ⬜ Email should be triggered once task is assigned or updated to all users mapped for the project

## Implementation Status

### ✅ Phase 1: Data Model Enhancements (COMPLETE)

**Models Created:**
- `TaskTemplate.cs` - Stores predefined task templates with category, team assignment, priority
- `TaskComment.cs` - Enables commenting on tasks
- `TaskDocument.cs` - Supports file attachments on tasks

**Models Updated:**
- `TaskAssignment.cs` - Added navigation properties for Comments and Documents collections

**Database Seed Data:**
- 29 predefined task templates across 5 categories:
  - Platinum CS Checklist (7 tasks)
  - Gold CS Checklist (6 tasks)
  - Silver CS Checklist (5 tasks)
  - Marketing Team Checklist (5 tasks)
  - TIM Team Checklist (6 tasks)

**Files Modified:**
- `Models/TaskTemplate.cs` (new)
- `Models/TaskComment.cs` (new)
- `Models/TaskDocument.cs` (new)
- `Models/TaskAssignment.cs` (updated)
- `Data/ApplicationDbContext.cs` (added DbSets and seed data)

### ⬜ Phase 2: Database Migration (TODO)

**Next Steps:**
```bash
cd VisitManagement
dotnet ef migrations add AddTaskManagementEnhancements
```

This will create migration for:
- TaskTemplates table
- TaskComments table
- TaskDocuments table
- Navigation properties

### ⬜ Phase 3: Admin Template Management Controller (TODO)

**Create: `Controllers/TaskTemplatesController.cs`**

Key Actions:
- `Index()` - List all task templates
- `Create()` - Create new template
- `Edit(id)` - Edit existing template
- `Delete(id)` - Delete template
- `CreateTasksFromTemplate(visitId, category)` - Bulk create tasks for a visit from template

**Authorization:**
- Admin only access

### ⬜ Phase 4: Enhanced Task Assignment Controller (TODO)

**Update: `Controllers/TaskAssignmentsController.cs`**

New/Enhanced Actions:
- `CreateFromTemplate(visitId)` - Show template selection and create tasks
- `AddComment(taskId, comment)` - Add comment to task
- `UploadDocument(taskId, file)` - Upload document attachment
- `UpdateStatus(taskId, newStatus)` - Update task status (for drag-drop)
- `KanbanBoard(visitId)` - Display Kanban board for a visit

### ⬜ Phase 5: Kanban Board View (TODO)

**Create: `Views/TaskAssignments/KanbanBoard.cshtml`**

Features to implement:
- 5 columns for task statuses: Not Started, In Progress, Blocked, Completed, Cancelled
- Drag-and-drop functionality using SortableJS or similar library
- Filter by visit
- Real-time status updates via AJAX
- Visual indicators for priority (color coding)
- Task cards showing: name, assigned to, due date, priority

**JavaScript Libraries Needed:**
- SortableJS for drag-and-drop
- Or use HTML5 drag-and-drop with custom JavaScript

**Sample Structure:**
```html
<div class="kanban-board">
    <div class="kanban-column" data-status="NotStarted">
        <h3>Not Started (5)</h3>
        <div class="kanban-cards">
            <!-- Task cards here -->
        </div>
    </div>
    <div class="kanban-column" data-status="InProgress">
        <h3>In Progress (3)</h3>
        <div class="kanban-cards">
            <!-- Task cards here -->
        </div>
    </div>
    <!-- More columns -->
</div>
```

### ⬜ Phase 6: Admin Template Selection View (TODO)

**Create: `Views/TaskAssignments/CreateFromTemplate.cshtml`**

Features:
- Visit dropdown or selection (pre-filtered by current visit)
- Category selection based on visit category (auto-detect from visit)
- Display matching templates in a checklist format
- Allow admin to:
  - Select/deselect individual tasks
  - Modify task properties (due date, assigned person)
  - Add custom tasks not in template
- Bulk create button

### ⬜ Phase 7: Task Details Enhancement (TODO)

**Update: `Views/TaskAssignments/Details.cshtml`**

Add sections for:
1. **Comments Section**
   - Display all comments with user and timestamp
   - Add new comment form
   - Real-time comment loading

2. **Documents Section**
   - List all uploaded documents
   - Upload new document form
   - Download links
   - File size and uploader info

3. **Activity Log**
   - Track status changes
   - Track assignments
   - Track comments and documents

### ⬜ Phase 8: File Upload Service (TODO)

**Create: `Services/FileUploadService.cs`**

Methods:
- `UploadTaskDocument(file, taskId, userId)` - Upload and save file
- `DeleteTaskDocument(documentId)` - Delete file
- `GetDocument Path(documentId)` - Get file path for download

**Storage:**
- Store in `wwwroot/uploads/task-documents/`
- Generate unique filenames
- Validate file types and sizes
- Implement security (don't allow executable files)

### ⬜ Phase 9: Enhanced Email Notifications (TODO)

**Update email triggers in:**
- `Controllers/TaskAssignmentsController.cs`
- `Services/EmailService.cs`

**New Email Events:**
1. Task Status Changed - notify assigned person and team
2. Comment Added - notify all users involved in task
3. Document Uploaded - notify assigned person
4. Task Due Soon (background job)

**Create new email templates:**
- Task Status Updated
- Task Comment Added
- Task Document Uploaded

### ⬜ Phase 10: Navigation and Integration (TODO)

**Updates needed:**

1. **Visit Details View** (`Views/Visits/Details.cshtml`)
   - Add "Manage Tasks" button linking to Kanban board
   - Display task summary (total, completed, in progress, blocked)

2. **Navigation Menu** (`Views/Shared/_Layout.cshtml`)
   - Add "Task Templates" link (Admin only)
   - Update "Task Assignments" to have submenu:
     - List All Tasks
     - Kanban Board
     - Templates (Admin)

3. **Dashboard** (`Views/Home/Index.cshtml`)
   - Add "My Tasks" widget showing assigned tasks
   - Add "Overdue Tasks" widget
   - Add task completion statistics

### ⬜ Phase 11: AJAX and Client-Side Logic (TODO)

**JavaScript files to create:**

1. `wwwroot/js/kanban.js`
   - Drag-and-drop handlers
   - AJAX calls to update task status
   - Real-time board updates

2. `wwwroot/js/task-comments.js`
   - Add comment via AJAX
   - Load comments dynamically
   - Real-time comment updates

3. `wwwroot/js/task-documents.js`
   - Upload documents via AJAX
   - Progress indicators
   - Delete confirmation

**CSS files to create:**

1. `wwwroot/css/kanban.css`
   - Kanban board styling
   - Column layouts
   - Card designs
   - Drag-and-drop visual feedback

## API Endpoints Needed

### Task Template Management
- `GET /TaskTemplates` - List templates
- `GET /TaskTemplates/Create` - Create form
- `POST /TaskTemplates/Create` - Save template
- `GET /TaskTemplates/Edit/{id}` - Edit form
- `POST /TaskTemplates/Edit/{id}` - Update template
- `POST /TaskTemplates/Delete/{id}` - Delete template

### Task Creation from Templates
- `GET /TaskAssignments/CreateFromTemplate?visitId={id}` - Template selection page
- `POST /TaskAssignments/BulkCreateFromTemplate` - Bulk create tasks

### Kanban Board
- `GET /TaskAssignments/KanbanBoard?visitId={id}` - Kanban board view
- `POST /TaskAssignments/UpdateStatus` - AJAX update status (for drag-drop)

### Comments and Documents
- `POST /TaskAssignments/AddComment` - Add comment (AJAX)
- `POST /TaskAssignments/UploadDocument` - Upload document (AJAX)
- `GET /TaskAssignments/DownloadDocument/{id}` - Download document
- `POST /TaskAssignments/DeleteDocument/{id}` - Delete document

## Database Schema

### TaskTemplates Table
```sql
CREATE TABLE TaskTemplates (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(500) NOT NULL,
    Description NVARCHAR(2000),
    Category NVARCHAR(100) NOT NULL,
    AssignedToTeam NVARCHAR(100),
    Priority INT NOT NULL,
    EstimatedDays INT NOT NULL,
    DisplayOrder INT NOT NULL,
    IsActive BIT NOT NULL,
    CreatedDate DATETIME NOT NULL
);
```

### TaskComments Table
```sql
CREATE TABLE TaskComments (
    Id INT PRIMARY KEY IDENTITY,
    TaskAssignmentId INT NOT NULL,
    Comment NVARCHAR(2000) NOT NULL,
    CreatedBy NVARCHAR(256) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    FOREIGN KEY (TaskAssignmentId) REFERENCES TaskAssignments(Id) ON DELETE CASCADE
);
```

### TaskDocuments Table
```sql
CREATE TABLE TaskDocuments (
    Id INT PRIMARY KEY IDENTITY,
    TaskAssignmentId INT NOT NULL,
    FileName NVARCHAR(500) NOT NULL,
    FilePath NVARCHAR(1000) NOT NULL,
    FileSize BIGINT NOT NULL,
    UploadedBy NVARCHAR(256) NOT NULL,
    UploadedDate DATETIME NOT NULL,
    FOREIGN KEY (TaskAssignmentId) REFERENCES TaskAssignments(Id) ON DELETE CASCADE
);
```

## Testing Checklist

### Unit Tests
- [ ] TaskTemplate CRUD operations
- [ ] Task creation from templates
- [ ] Comment addition
- [ ] Document upload/download
- [ ] Status update validation

### Integration Tests
- [ ] Bulk task creation workflow
- [ ] Kanban drag-and-drop
- [ ] Email notification triggers
- [ ] File upload security

### UI Tests
- [ ] Kanban board drag-and-drop functionality
- [ ] Comment form submission
- [ ] Document upload progress
- [ ] Template selection and customization

## Security Considerations

1. **Authorization**
   - Only admins can manage templates
   - Users can only update tasks assigned to them or their visits
   - Document access restricted to task participants

2. **File Upload Security**
   - Validate file types (only documents, no executables)
   - Limit file sizes (e.g., 10MB max)
   - Sanitize filenames
   - Store outside web root or with restricted access

3. **SQL Injection Prevention**
   - Use parameterized queries (Entity Framework handles this)
   - Validate all user inputs

4. **XSS Prevention**
   - HTML encode all user content
   - Sanitize rich text in comments

## Performance Optimizations

1. **Database Queries**
   - Use `Include()` for eager loading related data
   - Index frequently queried fields (VisitId, Status, Category)
   - Pagination for large task lists

2. **File Storage**
   - Consider cloud storage (Azure Blob, AWS S3) for production
   - Implement file size limits
   - Compress large files

3. **AJAX Requests**
   - Implement debouncing for drag-and-drop
   - Use loading indicators
   - Cache task data client-side

## Next Steps for Implementation

1. **Create database migration** for new tables
2. **Implement TaskTemplatesController** (admin-only)
3. **Enhance TaskAssignmentsController** with new actions
4. **Create Kanban board view** with drag-drop
5. **Implement file upload service**
6. **Add comment and document features**
7. **Enhance email notifications**
8. **Add navigation and dashboard widgets**
9. **Write comprehensive tests**
10. **Deploy and gather user feedback**

## Estimated Timeline

- Phase 2-3 (Controllers): 4-6 hours
- Phase 4-5 (Kanban UI): 6-8 hours
- Phase 6-7 (Enhanced Views): 4-6 hours
- Phase 8 (File Upload): 2-3 hours
- Phase 9 (Email): 2-3 hours
- Phase 10-11 (Integration & JS): 4-6 hours
- Testing & Refinement: 4-6 hours

**Total: 26-38 hours of development**

## Current Status Summary

✅ **Completed**: Data models, database seed data
⬜ **In Progress**: Migration creation
⬜ **Remaining**: Controllers, views, Kanban UI, file upload, enhanced email, testing

The foundation is solid. The data model supports all requirements. The next critical steps are creating the migration and implementing the admin template management controller.
