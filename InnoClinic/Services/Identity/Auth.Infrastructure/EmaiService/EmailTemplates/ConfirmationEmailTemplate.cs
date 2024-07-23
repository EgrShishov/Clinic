public class ConfirmationEmailTemplate
{
    public string ConfirmationLink { get; set; }
    public string UserName { get; set; }
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
                    .content a {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 10px 20px;
                        font-size: 16px;
                        color: #ffffff;
                        background-color: #007bff;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    .content a:hover {{
                        background-color: #0056b3;
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
                        <h1>Account Confirmation</h1>
                    </div>
                    <div class='content'>
                        <p>Dear {UserName},</p>
                        <p>Thank you for registering. Please click the button below to confirm your email address:</p>
                        <a href='{ConfirmationLink}'>Confirm Email</a>
                        <p>If you did not create an account, please ignore this email.</p>
                    </div>
                    <div class='footer'>
                        <p>&copy; 2024 Your Company. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>
        ";
    }
}