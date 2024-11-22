# PROG6212 POE CMCS
Contract Monthly Claim System (CMCS)
Overview
The Contract Monthly Claim System (CMCS) is a web-based application designed to streamline the process of submitting and approving monthly claims for independent contractors. This project is part of a practical exercise to develop a user-friendly interface using ASP.NET Core MVC. The system allows lecturers to submit their work hours for approval, while coordinators and managers can review and approve or reject claims.

Features
Lecturer Claim Submission: Lecturers can submit claims by entering hours worked and uploading supporting documents.
Coordinator/Manager Claim Approval: Coordinators or managers can view, approve, or reject submitted claims.
Document Upload: Lecturers can upload supporting documents, such as work reports or timesheets, to their claims.

Part 2: Contract Monthly Claim System (CMCS)
Overview
This project aims to enhance the functionality of a Contract Monthly Claim System (CMCS) for managing claims submitted by lecturers. Part 2 focuses on implementing key features such as claim submission, approval, rejection, and document management, with an emphasis on improving user experience and system reliability.

Features
Claim Submission:

Lecturers can submit claims by providing necessary details, including hours worked, submission date, and a supporting document.
A document upload feature allows lecturers to attach relevant files to their claims.
Claim Management:

Academic managers can view all submitted claims and take appropriate actions (approve/reject).
The system tracks the status of each claim, with statuses including Pending, Approved, and Rejected.
Document Handling:

Supports uploading and storing supporting documents securely.
Provides a link to view the uploaded documents directly from the claims list.

i have also created a database using SSMS and stored the data from the web app in there.
User Interface:

A user-friendly interface for both lecturers and academic managers to navigate and perform actions efficiently.
Technologies Used
ASP.NET Core MVC: For building the web application.
Entity Framework Core: For database interactions and data management.
SQL Server: For storing claim and lecturer data.
HTML/CSS: For styling the application and enhancing the user interface.



Setup Instructions
Prerequisites
To run the project locally, you will need the following:

Visual Studio 2019/2022 or later
ASP.NET Core SDK version 5.0 or later
Git (for version control)
Steps to Run the Project Locally
Clone the Repository Clone the project from GitHub using the following command:

bash
Copy code
git clone https://github.com/ilyaas2004/PROG6212-POE-CMCS.git
Open the Project in Visual Studio Navigate to the project folder and open the solution file (.sln) in Visual Studio.

Restore Dependencies In Visual Studio, restore the project dependencies using the NuGet Package Manager.

Run the Application Press F5 or click Start in Visual Studio to run the project. The application will start in your default web browser.

Project Structure
/Models: Contains the data models (e.g., Lecturer, Claim, Document) that represent the entities in the system.
/Views: Contains the Razor views (UI) for the lecturer claim submission, claim approval, and document upload forms.
/Controllers: Contains the controllers responsible for handling user requests and returning the appropriate views.
/wwwroot: Contains static files such as custom CSS and JavaScript.
