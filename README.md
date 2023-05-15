# Token Generator

## Points

When you run it (you can use VSCode, VisualStudio, etc), the API will use port 5000 by default (Program.cs).

You can access it through http://localhost:5000/swagger

** I put both Controllers in the same app because I could not figure out yet how could 2 projects share the same database "in-memory". And I didn't want to open endpoints to share it.

## Description

This project aims to register cards, generate a token for each one, and validate a token.
It should do a token generation for cashless registration.

There should be an API that receive customer card and save it on the db; and another API that validate that token based on data provided in the request.

It should show some examples of:
- • Immutability;
- • Concurrency handling;
- • Separation of concerns;
- • Unit and integration tests;
- • API design;
- • Code readability;
- • Error handling


