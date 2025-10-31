@echo off
@echo This cmd file creates a Data API Builder configuration based on the chosen database objects.
@echo To run the cmd, create an .env file with the following contents:
@echo dab-connection-string=your connection string
@echo ** Make sure to exclude the .env file from source control **
@echo **
dotnet tool install -g Microsoft.DataApiBuilder
dab init -c dab-config.json --database-type mssql --connection-string "@env('dab-connection-string')" --host-mode Development
@echo Adding tables
dab add "AuditLog" --source "[dbo].[AuditLogs]" --fields.include "Id,Action,TimeOfAction,EntityName,EntityId,OldValues,NewValues,IpAddress,UserAgent,RefId" --permissions "anonymous:*" 
dab add "CartItem" --source "[dbo].[CartItems]" --fields.include "Id,UserId,ScheduleId,RefId" --permissions "anonymous:*" 
dab add "Category" --source "[dbo].[Categories]" --fields.include "Id,Name,LongName,Description,ImageUrl,IsActive,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "Course" --source "[dbo].[Courses]" --fields.include "Id,Name,Description,ImageUrl,Price,IsActive,CategoryId,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "InvoiceDetail" --source "[dbo].[InvoiceDetails]" --fields.include "Id,InvoiceId,CourseName,CategoryName,Price,ScheduleDate,RefId" --permissions "anonymous:*" 
dab add "Invoice" --source "[dbo].[Invoices]" --fields.include "Id,RefCode,CreatedAt,UserId,PaymentMethodId,RefId" --permissions "anonymous:*" 
dab add "MyClass" --source "[dbo].[MyClasses]" --fields.include "Id,UserId,ScheduleId,RefId" --permissions "anonymous:*" 
dab add "PaymentMethod" --source "[dbo].[PaymentMethods]" --fields.include "Id,Name,LogoUrl,IsActive,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "RoleClaim" --source "[dbo].[RoleClaims]" --fields.include "Id,RoleId,ClaimType,ClaimValue,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "Role" --source "[dbo].[Roles]" --fields.include "Id,Name,Description,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "Schedule" --source "[dbo].[Schedules]" --fields.include "Id,Date,CreatedAt,UpdatedAt,CourseId,RefId" --permissions "anonymous:*" 
dab add "UserClaim" --source "[dbo].[UserClaims]" --fields.include "Id,UserId,ClaimType,ClaimValue,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "UserRole" --source "[dbo].[UserRoles]" --fields.include "Id,UserId,RoleId,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
dab add "User" --source "[dbo].[Users]" --fields.include "Id,IsActive,Email,Name,PasswordHash,EmailConfirmed,RefreshToken,RefreshTokenExpiry,EmailConfirmationToken,EmailConfirmationTokenExpiry,PasswordResetToken,PasswordResetTokenExpiry,FailedLoginAttempts,LockoutEnd,LastLoginAt,RefId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy" --permissions "anonymous:*" 
@echo Adding views and tables without primary key
@echo Adding relationships
dab update AuditLog --relationship User --target.entity User --cardinality one
dab update User --relationship AuditLog --target.entity AuditLog --cardinality many
dab update CartItem --relationship Schedule --target.entity Schedule --cardinality one
dab update Schedule --relationship CartItem --target.entity CartItem --cardinality many
dab update CartItem --relationship User --target.entity User --cardinality one
dab update User --relationship CartItem --target.entity CartItem --cardinality many
dab update Course --relationship Category --target.entity Category --cardinality one
dab update Category --relationship Course --target.entity Course --cardinality many
dab update InvoiceDetail --relationship Invoice --target.entity Invoice --cardinality one
dab update Invoice --relationship InvoiceDetail --target.entity InvoiceDetail --cardinality many
dab update Invoice --relationship PaymentMethod --target.entity PaymentMethod --cardinality one
dab update PaymentMethod --relationship Invoice --target.entity Invoice --cardinality many
dab update Invoice --relationship User --target.entity User --cardinality one
dab update User --relationship Invoice --target.entity Invoice --cardinality many
dab update MyClass --relationship Schedule --target.entity Schedule --cardinality one
dab update Schedule --relationship MyClass --target.entity MyClass --cardinality many
dab update MyClass --relationship User --target.entity User --cardinality one
dab update User --relationship MyClass --target.entity MyClass --cardinality many
dab update RoleClaim --relationship Role --target.entity Role --cardinality one
dab update Role --relationship RoleClaim --target.entity RoleClaim --cardinality many
dab update Schedule --relationship Course --target.entity Course --cardinality one
dab update Course --relationship Schedule --target.entity Schedule --cardinality many
dab update UserClaim --relationship User --target.entity User --cardinality one
dab update User --relationship UserClaim --target.entity UserClaim --cardinality many
dab update UserRole --relationship Role --target.entity Role --cardinality one
dab update Role --relationship UserRole --target.entity UserRole --cardinality many
dab update UserRole --relationship User --target.entity User --cardinality one
dab update User --relationship UserRole --target.entity UserRole --cardinality many
@echo Adding stored procedures
@echo **
@echo ** run 'dab validate' to validate your configuration **
@echo ** run 'dab start' to start the development API host **
