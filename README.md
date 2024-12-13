# Event Media Hub Group Project - README
Project Overview
This project is an Event Media Hub application designed to manage events, users, photos, categories, and locations. The application allows users to create, update, view, and delete records in these entities. It also includes authentication and authorization for secure access, along with well-structured views for each entity.

Key Features:
User Authentication: Secure sign-up, login, and account management.
CRUD Operations: Ability to create, read, update, and delete records for the following entities:
Photo
Event
User
Category
Location
MVC Pattern: The project follows the Model-View-Controller architecture.
Styling: Custom CSS applied for improved UI design.
Table Structure
The project includes five main entities stored in the database:

User: Manages user information including username, email, and roles.
Event: Stores information about events such as name, description, and associated category.
Location: Manages event locations.
Photo: Stores event-related photos.
Category: Categorizes events.
Relations:
Event and User: Each event is associated with a user (creator).
Event and Location: Each event has a location.
Photo and User: Each photo is associated with a user (owner).
Project Structure
Controllers: Responsible for handling HTTP requests and interacting with services.
Services: Contains business logic and interacts with the database through interfaces.
Interfaces: Define methods for services that are implemented in the corresponding service classes.
Models: Represent the data structure for the tables (User, Event, Photo, Category, Location).
Views: Views for rendering data for each entity (CRUD operations).
Contributors
This project was a joint effort between two contributors:

Zalak: Led the development of the authentication system, including user registration, login, and management. Zalak also contributed to the overall architecture of the system, implementing services and controllers, and handling the majority of the views.
Vishnu: Focused on the styling of the application, including custom CSS to enhance the user interface. Vishnu contributed to services, controllers, and views.


Setup and Installation
To run this project on Visual Studio 2022, follow these steps:

Clone the Repository:

Open Visual Studio 2022 and follow these steps:

Click on File > Clone or check out code.
Enter the repository URL.
Choose a local directory and click Clone.
Install Dependencies:

The project uses NuGet packages for dependencies. To restore the NuGet packages, do the following:

Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
Run the following command:dotnet restore

Configure the Database:

Add the connection string for the database in appsettings.json under ConnectionStrings -> DefaultConnection.
To create the database schema, run the following commands in Package Manager Console:dotnet ef migrations add InitialCreate
dotnet ef database update
Run the Application:

Press F5 or click on the Run button in Visual Studio.
The application will start locally. Open your browser and go to http://localhost:7056.
You can also use the IIS Express to run the application.

Authentication (Implemented by Zalak)
Sign Up: Users can register by providing their username, email, and password.
Login: Registered users can log in to access their account and manage events.
Roles: The application includes role-based authentication, allowing different levels of access for users.
Account Management: Users can view and update their account details.
CRUD Operations
For each of the entities (User, Event, Photo, Category, Location), CRUD operations are implemented:

Create:
Allows the creation of new records for each entity. For example, users can create new events, categories, photos, and locations.
Read:
Displays detailed views for each entity, including the relevant data (e.g., event name, description, category, etc.).
Update:
Users can update existing records, such as editing event details or changing the photo associated with an event.
Delete:
Allows the deletion of records. Users can delete their events, photos, categories, or locations.
Controller, Service, and Interface Contribution
The following breakdown of work for controllers, services, and interfaces is provided according to Zalak's and Vishnu's contributions:

Controllers:
Zalak: Led the controller development for authentication (login, registration), user management, and event controllers.
Vishnu: Contributed to event, location, and category controllers, integrating business logic with services.
Services:
Zalak: Developed services for user management, event handling, and authentication-related functionality. Implemented service logic to communicate with the database.
Vishnu: Assisted with services for category, location, and photo management. Implemented CRUD operations for these entities.
Interfaces:
Zalak: Designed interfaces for user, event, and authentication services.
Vishnu: Worked on defining interfaces for location, category, and photo services.
Views:
Zalak: Designed views for authentication, user profile management, and event CRUD operations.
Vishnu: Developed views for the category, location, and photo management sections. Implemented dynamic card-based layouts for entities.
Styling (Implemented by Vishnu)
Vishnu focused on improving the user interface through CSS. The custom styling is applied across the entire application to provide a seamless user experience. Some key styling elements include:

Modern form layouts.
Responsive design for mobile and desktop views.
Enhanced card views for entities (events, users, photos, etc.).

Future Enhancements
User Profiles: Implement detailed user profiles for managing user-specific data.
Event Management: Add more features for event management, such as event scheduling, ticketing, etc.
Search & Filter: Implement search functionality for users to search events, photos, and categories.
