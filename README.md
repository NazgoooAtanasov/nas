# nas-shortener
## nas-shortener is a URI shortener created entirely in C# and ASP Net Core MVC.


The web app is designed around that there should not be more than one unique shortened link. 

The application checks if there is already shortened link with the slug that is passed in the form which is in the index page, so there should not be two equivalent links.

For storing the shortened links I use PostgreSQL.

- [x] Usage of MVC.
- [x] Proper database configuration.
- [x] Routing between the different pages.
- [x] Validating the input.
- [x] Checking for already existing slug.
- [x] Returning proper messages(responses).

---
##### Title: "nas-shortener"

##### Author: "NazgoooAtanasov" GitHub: https://github.com/NazgoooAtanasov