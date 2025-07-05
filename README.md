# 📢 Easy Complaint System

A professional and modern **Complaint Management System** built using **ASP.NET Core Web API** and **React**, following the principles of **Clean Architecture (Onion Architecture)**.  
This system enables users to submit complaints easily while providing full control for administrators to manage and resolve them efficiently — following a simple **ticket-based workflow**.

---

## 🚀 Features

- **📝 Complaint Submission with Attachments**  
  Users can submit complaints with detailed descriptions and attach files (images, documents, etc.).

- **👥 User Authentication & Roles**  
  - Secure login and registration  
  - Roles include: Admin, Employee, and User

- **🔄 Ticket Management System**  
  - Admin assigns complaints to responsible employees  
  - Manual escalation if the complaint is not resolved  
  - Workflow based on complaint type

- **📂 Complaint Tracking**  
  - Users can track status updates  
  - Admin/Employee can update and resolve complaints

- **📧 Email Notifications**  
  - Integrated with **Mailket** for sending password reset links and email confirmations

- **🔐 Validation & Security**  
  - FluentValidation for back-end input validation  
  - JWT + Microsoft Identity for secure authentication

- **📊 Admin Dashboard (Back-End APIs)**  
  Admins can manage users, complaints, workflows, and attachments from a central point

- **🧪 API Testing**  
  - Fully documented with **Swagger UI**

---

## 🛠️ Tech Stack

| Layer         | Technology                                |
|---------------|--------------------------------------------|
| Language      | C# (.NET 8)                                |
| Back-End      | ASP.NET Core Web API                       |
| Front-End     | React.js                                   |
| Database      | SQL Server                                 |
| Architecture  | Onion Architecture                         |
| ORM           | Entity Framework Core                      |
| Auth          | ASP.NET Identity + JWT                     |
| Validation    | FluentValidation                           |
| Mail Service  | Mailket                                    |
| API Testing   | Swagger                                     |

---

## ⚡ Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for running the React front-end)
- SQL Server (LocalDB or full version)

---

### 🧩 Back-End Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/MohamedElzoghby30/Complaint_System.git
   cd Complaint_System
2. **Update the database connection**

Open appsettings.json
Set your connection string

3. **Run Migrations**

dotnet ef database update

5. **Run the API**

dotnet run

## 👤 Roles & Usage
User:

Register, login

Submit & track complaints

Reset password via email

Admin:

Manage users

Assign & escalate complaints

Configure workflows

View reports

Employee:

View assigned complaints

Update status & handle resolution

## 🤝 Contribution
Contributions are welcome!
Feel free to fork the repo, open issues, or submit pull requests.

📄 License
MIT License (or specify your license)

## 👥 Team
**Mohamed Elzoghby – Back-End Developer**

**Ahmed Ihab Zaki – Back-End Developer**




 
