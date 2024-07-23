public class CredentialsEmailTemplate
{
    public string Email { get; set; }
    public string Password { get; set; }

    public string GetTemplate()
    {
        return $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        width: 100%;
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #ffffff;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        border-radius: 8px;
                        overflow: hidden;
                    }}
                    .header {{
                        background-color: #007bff;
                        color: #ffffff;
                        padding: 20px;
                        text-align: center;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                    }}
                    .content {{
                        padding: 20px;
                        text-align: center;
                    }}
                    .content p {{
                        font-size: 16px;
                        line-height: 1.5;
                        color: #333333;
                    }}
                    .credentials {{
                        margin: 20px 0;
                    }}
                    .credentials p {{
                        font-size: 18px;
                        font-weight: bold;
                        color: #555555;
                        margin: 10px 0;
                    }}
                    .footer {{
                        background-color: #f4f4f4;
                        color: #888888;
                        padding: 10px;
                        text-align: center;
                        font-size: 12px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Welcome!</h1>
                    </div>
                    <div class='content'>
                        <p>Dear user,</p>
                        <p>Your account has been created successfully. Below are your login credentials:</p>
                        <div class='credentials'>
                            <p>Email: {{Email}}</p>
                            <p>Password: {{Password}}</p>
                        </div>
                        <p>Please keep this information safe and do not share it with anyone.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; 2024 InnoClinic. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>
        ";
    }
}
