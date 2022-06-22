## Test user

username: john123

password: 12345678

## Endpoints

Login:

```jsonc
POST /api/auth/login

Request body:

{
    "Username": "john123",
    "Password": "12345678"
}
```

List users (require Basic Auth):

```
GET /api/user/list
```

List notas (require Basic Auth):

```
GET /api/note/list
```

Note details (require JWT):

```
GET /api/note/[note_id]
```

Create note (require JWT):

```jsonc
POST /api/note

Request body:

{
    "Title": "Keep in mind",
    "Content": "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
    "Priority": 1,
    "UserId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxx"
}
```

Update note (require JWT):

```jsonc
PUT /api/note/[note_id]

Request body:

{
    "Title": "Keep in mind",
    "Content": "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
    "Priority": 1
}
```