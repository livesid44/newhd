# Email Configuration Guide

## Overview

The Visit Management System now supports comprehensive email notifications with configurable recipients (TO, CC, BCC) for all visit-related activities.

## Features

### 1. Configurable Recipients

Every email template can have its own recipient configuration:
- **TO Recipients**: Primary recipients who should receive the email
- **CC Recipients**: People who should be copied on the email
- **BCC Recipients**: People who should receive blind copies

If template-specific recipients are not configured, the system falls back to SMTP default recipients.

### 2. Professional Email Templates

Seven professionally designed email templates are available:

| Template Type | Purpose | When Sent | Color Theme |
|---------------|---------|-----------|-------------|
| Visit Created | New visit notification | When visit is created | Red (#e31837) |
| Visit Updated | Changes notification | When visit is modified | Yellow (#ffc107) |
| Visit Confirmed | Confirmation notice | When status changes to Confirmed | Green (#28a745) |
| Visit Reminder | 24-hour reminder | 1 day before visit | Blue (#17a2b8) |
| Visit Completed | Post-visit activities | When visit is marked complete | Gray (#6c757d) |
| Task Assigned | Task assignment | When task is assigned | Red (#e31837) |
| Task Due Soon | Urgent deadline reminder | When task due date approaches | Dark Red (#dc3545) |

## Setup Instructions

### Step 1: Configure SMTP Settings

1. Navigate to **Settings** → **Configure SMTP**
2. Fill in your email server details:
   - SMTP Server (e.g., smtp.gmail.com)
   - Port (usually 587 for TLS)
   - From Email
   - From Name
   - Username (optional)
   - Password (optional)
3. Configure default recipients (optional):
   - **Default TO Recipients**: Emails that should receive ALL notifications
   - **Default CC Recipients**: Emails that should be CC'd on ALL notifications
   - **Default BCC Recipients**: Emails that should be BCC'd on ALL notifications
4. Enable SSL and Notifications
5. Click **Save Settings**

### Step 2: Test Email Configuration

1. Navigate to **Settings** → **Test Email**
2. Enter your email address
3. Click **Send Test Email**
4. Check your inbox to verify SMTP settings are working

### Step 3: Create Email Templates

#### Option A: Use Provided Templates (Recommended)

1. Open `/VisitManagement/wwwroot/email-templates-reference.html` in a text editor
2. Copy the HTML for desired template type
3. Navigate to **Settings** → **Create Template**
4. Fill in the form:
   - **Template Name**: Descriptive name (e.g., "Visit Created Notification")
   - **Template Type**: Select from dropdown (e.g., "Visit Created")
   - **Subject**: Add subject line with placeholders (e.g., "New Visit Scheduled - {AccountName}")
   - **Body**: Paste the copied HTML
   - **To Recipients**: (optional) Specific emails for this template type
   - **CC Recipients**: (optional) Specific CCs for this template type
   - **BCC Recipients**: (optional) Specific BCCs for this template type
   - **Is Active**: Check to enable
5. Click **Create Template**

#### Option B: Create Custom Template

1. Navigate to **Settings** → **Create Template**
2. Write your own HTML email body
3. Use available placeholders (see below)
4. Configure recipients
5. Save template

### Step 4: Configure Recipients

You have two levels of recipient configuration:

#### Global (SMTP) Recipients

Set in **Configure SMTP** → These apply to ALL emails unless overridden:
```
Default TO Recipients:  manager@example.com; team@example.com
Default CC Recipients:  backup@example.com
Default BCC Recipients: archive@example.com
```

#### Template-Specific Recipients

Set in each template → These override SMTP defaults:
```
Template: Visit Created
TO Recipients:  sales.team@example.com; operations@example.com
CC Recipients:  ceo@example.com
BCC Recipients: compliance@example.com
```

**Priority**: Template recipients → SMTP defaults → Error if no TO recipients

## Available Placeholders

Use these placeholders in email subject and body:

### Visit Placeholders
- `{AccountName}` - Client/account name
- `{VisitDate}` - Visit date (formatted as DD/MM/YYYY)
- `{Location}` - Visit location
- `{Category}` - Visit category (Platinum/Gold/Silver)
- `{SalesSpoc}` - Sales SPOC name
- `{OpportunityType}` - Opportunity type (NN/EN)
- `{NameAndAttendees}` - Visitor names and attendee count

### Task Placeholders
- `{TaskName}` - Task name
- `{AssignedTeam}` - Team assigned to the task
- `{DueDate}` - Task due date
- `{Priority}` - Task priority level
- `{TaskDescription}` - Detailed task description
- `{TaskStatus}` - Current status of the task

## Email Recipient Examples

### Example 1: Department-Wide Notifications

**Scenario**: All sales team should know about new visits

**Configuration**:
```
Template Type: Visit Created
TO Recipients: sales-team@techmahindra.com
CC Recipients: sales-manager@techmahindra.com
BCC Recipients: (empty)
```

### Example 2: Multi-Level Notifications

**Scenario**: Notify specific people with management oversight

**Configuration**:
```
Template Type: Visit Confirmed
TO Recipients: sales.spoc@example.com; client.experience@example.com
CC Recipients: vertical.head@example.com; account.owner@example.com
BCC Recipients: compliance@example.com; reporting@example.com
```

### Example 3: Using Defaults

**Scenario**: Let SMTP settings handle all recipients

**Configuration**:
```
Template Type: Visit Updated
TO Recipients: (leave empty)
CC Recipients: (leave empty)
BCC Recipients: (leave empty)

SMTP Settings:
Default TO Recipients: all-visits@example.com
Default CC Recipients: manager@example.com
Default BCC Recipients: archive@example.com
```

### Example 4: Multiple Recipients

**Scenario**: Send to multiple people in each category

**Configuration**:
```
TO Recipients: person1@example.com, person2@example.com; person3@example.com
CC Recipients: cc1@example.com, cc2@example.com
BCC Recipients: bcc1@example.com; bcc2@example.com
```

Note: You can use commas OR semicolons to separate email addresses.

## Troubleshooting

### Emails Not Sending

1. **Check SMTP Settings**:
   - Verify server, port, credentials
   - Ensure "Enable Notifications" is checked
   - Test with **Test Email** function

2. **Check Template**:
   - Ensure template is marked as "Active"
   - Verify template type matches the trigger
   - Check that at least TO recipients are configured

3. **Check Logs**:
   - Review application logs for error messages
   - Look for connection or authentication errors

### Wrong Recipients

1. **Check Template Recipients**:
   - Template-specific recipients override SMTP defaults
   - Verify recipient configuration in template

2. **Check SMTP Defaults**:
   - If template has no recipients, SMTP defaults are used
   - Verify default recipient configuration

### Missing Placeholders

1. **Check Spelling**:
   - Placeholders are case-sensitive
   - Must be exactly `{AccountName}` not `{accountname}`

2. **Check Template Type**:
   - Task placeholders only work for task-related templates
   - Visit placeholders only work for visit-related templates

## Security Best Practices

1. **Use BCC for Archive**:
   - Add archive@ or compliance@ email to BCC
   - Recipients won't see archive addresses

2. **Separate Environments**:
   - Use different recipient lists for dev/test/prod
   - Test with internal emails first

3. **Limit TO Recipients**:
   - Only send TO critical people
   - Use CC for informational recipients

4. **Password Protection**:
   - Use app-specific passwords for Gmail
   - Don't store plain passwords in config files
   - Use environment variables in production

## Email Templates Customization

### Modify Existing Template

1. Navigate to **Settings**
2. Find template in list
3. Click **Edit**
4. Modify HTML, subject, or recipients
5. Click **Save Changes**

### Deactivate Template

1. Navigate to **Settings**
2. Find template in list
3. Click **Edit**
4. Uncheck **Is Active**
5. Click **Save Changes**

### Delete Template

1. Navigate to **Settings**
2. Find template in list
3. Click **Delete**
4. Confirm deletion

## Workflow Examples

### Workflow 1: New Visit Created

1. User creates a visit in the system
2. System finds active "VisitCreated" template
3. System replaces placeholders with visit data
4. System sends email to:
   - Template TO recipients (if configured)
   - OR SMTP default TO recipients
   - PLUS Template CC recipients (if configured)
   - OR SMTP default CC recipients
   - PLUS Template BCC recipients (if configured)
   - OR SMTP default BCC recipients

### Workflow 2: Task Assignment

1. User assigns a task to a team
2. System finds active "TaskAssigned" template
3. System replaces task placeholders with task data
4. System sends notification following same recipient logic

## Maintenance

### Regular Tasks

1. **Review Templates Monthly**:
   - Ensure templates are still relevant
   - Update branding if needed
   - Check recipient lists are current

2. **Monitor Email Delivery**:
   - Check application logs
   - Verify emails are being received
   - Update SMTP settings if issues arise

3. **Update Recipients**:
   - Keep distribution lists current
   - Remove employees who have left
   - Add new team members

### Updates and Changes

When organizational changes occur:

1. **Team Restructuring**:
   - Update template recipients
   - Update SMTP default recipients
   - Test email delivery

2. **Process Changes**:
   - Create new template types if needed
   - Modify existing templates
   - Update workflows

3. **Branding Updates**:
   - Update email HTML templates
   - Change colors and logos
   - Test rendering in multiple email clients

## Support

For issues with email configuration:

1. Check this guide first
2. Review troubleshooting section
3. Check application logs
4. Test SMTP settings independently
5. Contact system administrator

---

**Version**: 1.0  
**Last Updated**: February 2024  
**Tech Mahindra Visit Management System**
