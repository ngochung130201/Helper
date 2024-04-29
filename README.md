# Project Helper
This is a helper project 
## REST API
The REST API to the example app is described below.
### Sent Mail
- [POST] /api/sent-mail
    + Body
    ``` json
        {
        "mailAccount": {
            "email": "string",
            "password": "string",
            "senderName": "string",
            "senderEmail": "string",
            "smtpServer": "string",
            "smtpPort": 0
        },
        "sentMail": {
            "to": "string",
            "subject": "string",
            "body": "string", // body contain {{otp}} with 6 characters if autoGenerateOpt = true
            "name": "string"
        },
    "autoGenerateOpt": true
    }
    ```
    - Properties
        + mailAccount : This is the mail account information.
            * Email: Email of the account
            * Password: Password of the account(app password)
            * SenderName: Sender name of the account
            * SenderEmail: Sender email of the account
            * SmtpServer: Smtp server of the account(smtp.gmail.com)
            * SmtpPort: Smtp port of the account(587)
        + sentMail : This is the sent mail information.
            * To: To address of the mail
            * Subject: Subject of the mail
            * Body: Body of the mail
            * Name: Name of the mail
        + autoGenerateOpt : This is to generate otp automatically or not.(default: false)
            - true: Generate otp 6 characters automatically and save to json file Assets/otps.json 5 min expired require format body contain {{otp}}.
    - Response
    ```json
        {
            "message": "Mail sending failed",
            "status": false,
        }
    ```
- [POST] /api/sent-mail/check-otp
    + Body
    ```json
        {
            "email": "string",
            "otp": "string",
            "now": "2022-04-29T13:56:18.4547505+07:00"
        }
    ```
    - Properties
        + email: Email of the account
        + otp: Otp of the account
        + now: Now time
    - Response
    ```json
        {
            "message": "Mail sending failed",
            "status": false,
        }
    ```