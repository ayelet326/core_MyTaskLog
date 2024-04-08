
# Task Management System
  
 ## System Description:
  The task management system is a web application based on the C# language and ASP.NET framework on the server side, and static JS, HTML and CSS on the client side, that facilitates the management of tasks and users. The system empowers users to create, edit, delete, and update their tasks. Additionally, system administrators possess the authority to manage other users on the platform, in addition to overseeing their own tasks. Integral to the system is its authentication and authorization mechanism, which incorporates JWT (JSON Web Token)-based authentication for secure access control.

## System Architecture:

### Description:
The system is built on a client-server architecture, with a division between the server-side and the client-side. The server is based on C# language and ASP.NET framework, while the client is based on static resources of JS, HTML, and CSS
### Client-side Responsibilities:

The client-side application, developed using HTML, CSS, and JavaScript, focuses on presenting the user interface (UI) and facilitating user interactions. Its primary purpose is to provide a visually appealing and intuitive interface for users to interact with the system. It communicates with the server-side APIs to perform various operations, such as creating, reading, updating, and deleting tasks and users. Additionally, the client-side application handles HTTP requests to the server and processes responses, updating the UI accordingly. Essentially, it is responsible for the visual presentation and user experience of the system

### Server-side Responsibilities:
The server-side API, implemented using ASP.NET MVC Web API , serves as the backend of the system. Its main role is to handle requests from the client-side application and perform necessary operations on the data. This includes managing tasks and users stored in JSON files or a database. The server-side API exposes endpoints that the client-side application interacts with, sending HTTP requests (e.g., GET, POST, PUT, DELETE) to perform CRUD operations. It processes these requests, interacts with the data storage (e.g., JSON files), and sends back responses containing relevant data or status codes. Overall, the server-side API is responsible for the data management and processing logic of the system

## Access Permissions:
### Description:
The system manages two main aspects of access permissions: regular user permissions and system administrator permissions.

### Regular User Permissions:
- Access to personal tasks page: A regular user can view, add, edit, and delete tasks associated only with them.
- Editing user details: A regular user can edit their username and password on their personal tasks page.

### System Administrator Permissions:
- Access to tasks pages and user management: The system administrator can view and manage tasks of other users, similar to how a regular user manages their own. Additionally, they can manage users within the system, including adding and deleting users.
- Navigation between tasks and user management pages: The system administrator can navigate between various pages in the system, including tasks and user management pages.

### Login Page:

- In case the user is not logged in or their token has expired, they will be redirected to the login page to log in again.
- Users can log in using their username and password, via their Google account, or by using Postman.

### User Interface: