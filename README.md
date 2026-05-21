# MvcMovie — ASP.NET Core MVC (W02 Assignment)

## Search / Filter Features

| Filter    | How to use              | Example URL                                                |
| --------- | ----------------------- | ---------------------------------------------------------- |
| Title     | Type in the Title field | `/Movies?searchString=ghost`                               |
| Genre     | Select from dropdown    | `/Movies?movieGenre=Comedy`                                |
| Year From | Enter a year number     | `/Movies?yearFrom=2000`                                    |
| Combined  | Use all three together  | `/Movies?searchString=the&movieGenre=Action&yearFrom=2005` |

---

## CRUD Operations

| Action  | URL                  | HTTP Method | Description           |
| ------- | -------------------- | ----------- | --------------------- |
| List    | /Movies              | GET         | Show all movies       |
| Create  | /Movies/Create       | GET + POST  | Add new movie         |
| Details | /Movies/Details/{id} | GET         | View movie details    |
| Edit    | /Movies/Edit/{id}    | GET + POST  | Modify existing movie |
| Delete  | /Movies/Delete/{id}  | GET + POST  | Remove movie          |
