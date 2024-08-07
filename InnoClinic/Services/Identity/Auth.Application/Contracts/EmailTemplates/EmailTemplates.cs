public static class EmailTemplates
{
    public class EmailConfirmationLinkTemplate
    {
        public string ConfirmationLink { get; set; }

        public string GetTemplate()
        {
            return @"<!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }
                            .email-container {
                                max-width: 600px;
                                margin: 0 auto;
                                background-color: #ffffff;
                                padding: 20px;
                                border-radius: 10px;
                                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                            }
                            .header {
                                text-align: center;
                                padding: 20px 0;
                            }
                            .header img {
                                width: 100px;
                            }
                            .content {
                                text-align: center;
                                padding: 20px;
                            }
                            .content h1 {
                                color: #333333;
                            }
                            .content p {
                                color: #666666;
                                line-height: 1.5;
                            }
                            .btn-container {
                                text-align: center;
                                padding: 20px;
                            }
                            .btn {
                                background-color: #007BFF;
                                color: white;
                                padding: 15px 25px;
                                text-decoration: none;
                                border-radius: 5px;
                                font-size: 16px;
                                display: inline-block;
                            }
                            .btn:hover {
                                background-color: #0056b3;
                            }
                            .footer {
                                text-align: center;
                                color: #999999;
                                padding: 20px;
                                font-size: 12px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class=""email-container"">
                            <div class=""header"">
                                <img src=""https://via.placeholder.com/100"">
                            </div>
                            <div class=""content"">
                                <h1>Welcome to InnoClinic!</h1>
                                <p>Thank you for signing up for our service. Please confirm your email address by clicking the button below:</p>
                            </div>
                            <div class=""btn-container"">
                                <a href=""{confirmation_link}"" class=""btn"">Confirm Email</a>
                            </div>
                            <div class=""footer"">
                                <p>If you did not sign up for this account, please ignore this email.</p>
                                <p>&copy; 2024 Inno Clinic. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
        }

        public string GetContent()
        {
            return GetTemplate()
                .Replace("configmation_link", ConfirmationLink);
        }
    }
}
