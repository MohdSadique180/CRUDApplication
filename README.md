# CrudApplication

A simple CRUD web application built using **ASP.NET Core MVC, C#, and SQLite**, with Google OAuth 2.0 authentication.

Users can sign in with their Google account and manage their own products. Each user's data is private and cannot be accessed or modified by other users.

## Features

- Google sign-in using OAuth 2.0
- Cookie-based authentication
- Create, Read, Update, and Delete (CRUD) products
- User-specific product management
- Server-side validation
- Anti-forgery protection on forms
- SQLite database created automatically on first run
- Responsive UI using Bootstrap 5

## Tech Stack

| Component | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 8 or newer) |
| Language | C# |
| Database | SQLite |
| ORM | Entity Framework Core |
| Authentication | Google OAuth 2.0 |
| Session | Cookie Authentication |
| Frontend | Razor Views, Bootstrap 5 |

## Prerequisites

Make sure you have the following installed:

- [.NET SDK 8 or newer](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- A Google account for creating OAuth credentials

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/MohdSadique180/CrudApplication.git
cd CrudApplication
```

### 2. Configure Google OAuth 2.0

1. Open the [Google Cloud Console](https://console.cloud.google.com/).
2. Create a new project.
3. Set up Google Auth Platform and configure the OAuth consent screen.
4. Select **External** as the audience type.
5. Under **Audience**, add your Gmail address as a test user.
6. Go to **Clients** and create a new OAuth client.
7. Select **Web application** as the application type.
8. Add the following authorized redirect URI:

```text
https://localhost:7100/signin-google
```

9. Copy your Google Client ID and Client Secret.

### 3. Configure User Secrets

Initialize user secrets:

```bash
dotnet user-secrets init
```

Add your Google OAuth credentials:

```bash
dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_CLIENT_SECRET"
```

**Important:** Replace the placeholder values with your actual credentials. Never commit your Google Client Secret to GitHub.

### 4. Trust the HTTPS Development Certificate

Run the following command once:

```bash
dotnet dev-certs https --trust
```

### 5. Run the Application

```bash
dotnet run --launch-profile https
```

Open the application in your browser:

```text
https://localhost:7100
```

Click **Sign in with Google** to authenticate and start managing your products.

## Application Pages

| URL | Description |
|---|---|
| `/` | Home page |
| `/Account/Login` | Sign-in page |
| `/Products` | View your products |
| `/Products/Create` | Add a new product |
| `/Products/Edit/{id}` | Edit a product |
| `/Products/Delete/{id}` | Delete a product with confirmation |

## Project Structure

```text
CrudApplication/
├── Controllers/
│   ├── AccountController.cs
│   ├── HomeController.cs
│   └── ProductsController.cs
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── Product.cs
│   └── ProductForm.cs
├── Views/
├── wwwroot/
│   └── css/
│       └── site.css
├── Program.cs
└── appsettings.json
```

## How Authentication Works

1. The user clicks **Sign in with Google**.
2. The application redirects the user to Google for authentication.
3. Google authenticates the user and redirects back to `/signin-google`.
4. ASP.NET Core creates an authentication cookie.
5. The user is redirected to the Products page.
6. Each product is associated with the authenticated user's Google ID.

All product operations filter data by the current user's Google ID, ensuring that users can only access and manage their own products.

## Troubleshooting

| Problem | Solution |
|---|---|
| `redirect_uri_mismatch` | Ensure the redirect URI in Google Console exactly matches `https://localhost:7100/signin-google`. |
| `access_denied` | Add your Gmail address under Google Auth Platform → Audience → Test users. |
| `invalid_client` | Verify your Google Client ID and Client Secret. |
| Google credentials not configured | Run the user-secrets commands from the project directory. |
| HTTPS certificate warning | Run `dotnet dev-certs https --trust` and restart your browser. |
| .NET version error | Install the .NET SDK version required by the project. |

## Security

- Google OAuth 2.0 is used for authentication.
- Authentication is maintained using secure cookies.
- User-specific filtering protects product data.
- Server-side validation helps prevent invalid input.
- Anti-forgery tokens protect form submissions.
- Google Client Secrets are stored using .NET User Secrets during development.

For production deployment, use environment variables or a secure secrets manager and configure the redirect URI for your production domain.

## License

This project is created for learning purposes. You are free to use, modify, and extend it.
