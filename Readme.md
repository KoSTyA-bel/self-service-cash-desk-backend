<h1>appsettings.json</h1>

```
    "JwtSettings": {
    "Key": "YOUR_KEY",
    "Admins": [
            {
            "Name": "ADMIN_LOGIN",
            "Password": "ADMIN_PASSWORD"
            }
        ]
    },
    "MailSenderSettings": {
        "Name": "YOUR_NAME",
        "Subject": "YOUR_SUBJECT",
        "Login": "YOUR_GMAIL_LOGIN",
        "Password": "YOUR_APP_PASSWORD",
        "SmtpServerUrl": "smtp.gmail.com",
        "SmtpServerPort": 587,
        "SmtpUseSsl": true,
        "MessageTemplate": "YOUR_MAIL_MESSAGE_TEMPLATE"
    }
```

<h1>Template hints</h1>

| Hint | Desctiprion |
| ---- | ----------- |
| {0}  | Chech id    |
| {1}  | Date        |
| {2}  | Amount      |
| {3}  | Discount    |
| {4}  | Total       |
| {5}  | Cart number |
| {6}  | Card        |
