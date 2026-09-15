# MVC Core Assignments – README

## Overview

This repository contains four ASP.NET Core Web API assignments demonstrating CRUD operations for an ERP Employee Management application.

### Assignments

| Assignment | Technology | Controller | Database Operations |
|---|---|---|---|
| 1 | Entity Framework Core | `EmployeeController` | Employee CRUD |
| 2 | Dapper | `EmployeeDapperController` | Employee CRUD |
| 3 | Entity Framework Core | `Employee2Controller` | Employee + WorkExperience CRUD |
| 4 | Dapper + SQL Queries | `EmployeeDapper2Controller` | Employee + WorkExperience CRUD |

---

# Assignment 1: Employee CRUD using Entity Framework Core

## Objective

Create an Employee Management service for an ERP application using **Entity Framework Core**.

The service supports:

- Add Employee
- Get All Employees
- Get Employee By ID
- Update Employee
- Delete Employee

## Employee Fields

- First Name
- Last Name
- Date of Birth
- Personal Email
- Mobile Number
- Postal Address
- Gender – `1 = Female`, `2 = Male`, `3 = Others`
- Country – India, United Kingdom, United States of America
- City
- Designation – Software Engineer, Senior Software Engineer, Team Leader, Project Leader, Project Manager, Vice President
- Basic Pay – decimal
- Need Transportation – bit
- Notes
- Username
- Password

**Controller:** `EmployeeController`

**Services implemented:** POST, GET All, GET By ID, PUT, DELETE

---

## 1a. Create Employee

### Endpoint

```http
POST /api/Employee
```

### Test Data

| Field | Value |
|---|---|
| First Name | Tony |
| Last Name | Stark |
| Date of Birth | 1992-12-05 |
| Personal Email | tony.stark@example.com |
| Mobile Number | +12025550147 |
| Postal Address | 300 Independence Ave, Washington DC |
| Gender | 2 – Male |
| Country | United States of America |
| City | Washington DC |
| Designation | Project Leader |
| Basic Pay | 118000.00 |
| Need Transportation | false |
| Notes | Joined from external transfer |
| Username | tonystark |
| Password | `<your-password>` |

### Request Body

```json
{
  "employeeId": 10,
  "firstName": "Tony",
  "lastName": "Stark",
  "dateOfBirth": "1992-12-05T00:00:00",
  "personalEmail": "tony.stark@example.com",
  "mobileNumber": "+12025550147",
  "postalAddress": "300 Independence Ave, Washington DC",
  "gender": 2,
  "country": "United States of America",
  "city": "Washington DC",
  "designation": "Project Leader",
  "basicPay": 118000.00,
  "needTransportation": false,
  "notes": "Joined from external transfer",
  "username": "tonystark",
  "password": "<your-password>"
}
```

### Expected Output

Returns `true` when the employee is successfully inserted.

### Response Body

```json
true
```

---

## 1b. Get All Employees

### Endpoint

```http
GET /api/Employee
```

### Request Body

None.

### Expected Output

Returns all employee records currently available in the database.

### Response Body

```json
[
  {
    "employeeId": 10,
    "firstName": "Tony",
    "lastName": "Stark",
    "dateOfBirth": "1992-12-05T00:00:00",
    "personalEmail": "tony.stark@example.com",
    "mobileNumber": "+12025550147",
    "postalAddress": "300 Independence Ave, Washington DC",
    "gender": 2,
    "country": "United States of America",
    "city": "Washington DC",
    "designation": "Project Leader",
    "basicPay": 118000.00,
    "needTransportation": false,
    "notes": "Joined from external transfer",
    "username": "tonystark",
    "password": "<your-password>"
  },
  {
    "employeeId": 11,
    "firstName": "Natasha",
    "lastName": "Romanoff",
    "dateOfBirth": "1994-11-22T00:00:00",
    "personalEmail": "natasha.romanoff@example.com",
    "mobileNumber": "+12025550143",
    "postalAddress": "742 Evergreen Terrace, Arlington",
    "gender": 1,
    "country": "United States of America",
    "city": "Arlington",
    "designation": "Software Engineer",
    "basicPay": 125000.00,
    "needTransportation": true,
    "notes": "Requires secure remote network access.",
    "username": "natromanoff",
    "password": "<your-password>"
  }
]
```

---

## 1c. Get Employee By ID

### Endpoint

```http
GET /api/Employee/10
```

### Test Data

**Employee ID:** `10`

### Request Body

None.

### Expected Output

Returns the employee with ID `10`.

### Response Body

```json
{
  "employeeId": 10,
  "firstName": "Tony",
  "lastName": "Stark",
  "dateOfBirth": "1992-12-05T00:00:00",
  "personalEmail": "tony.stark@example.com",
  "mobileNumber": "+12025550147",
  "postalAddress": "300 Independence Ave, Washington DC",
  "gender": 2,
  "country": "United States of America",
  "city": "Washington DC",
  "designation": "Project Leader",
  "basicPay": 118000.00,
  "needTransportation": false,
  "notes": "Joined from external transfer",
  "username": "tonystark",
  "password": "<your-password>"
}
```

### Not Found

Example:

```http
GET /api/Employee/99
```

Returns HTTP `404 Not Found` when the employee does not exist.

---

## 1d. Update Employee

### Endpoint

```http
PUT /api/Employee/10
```

### Test Data

- Employee ID: `10`
- Designation: `Project Manager`
- Basic Pay: `135000.00`
- Need Transportation: `true`

### Request Body

```json
{
  "employeeId": 10,
  "firstName": "Tony",
  "lastName": "Stark",
  "dateOfBirth": "1992-12-05T00:00:00",
  "personalEmail": "tony.stark@example.com",
  "mobileNumber": "+12025550147",
  "postalAddress": "300 Independence Ave, Washington DC",
  "gender": 2,
  "country": "United States of America",
  "city": "Washington DC",
  "designation": "Project Manager",
  "basicPay": 135000.00,
  "needTransportation": true,
  "notes": "Promoted to Project Manager.",
  "username": "tonystark",
  "password": "<your-password>"
}
```

### Expected Output

```json
true
```

---

## 1e. Delete Employee

### Endpoint

```http
DELETE /api/Employee/10
```

### Test Data

**Employee ID:** `10`

### Request Body

None.

### Expected Output

```json
true
```

### Not Found

Example:

```http
DELETE /api/Employee/99
```

Returns HTTP `404 Not Found` when the employee does not exist.

---

# Assignment 2: Employee CRUD using Dapper

## Objective

Create an Employee Management service using **Dapper**.

The service supports:

- Add Employee
- Get All Employees
- Get Employee By ID
- Update Employee
- Delete Employee

**Controller:** `EmployeeDapperController`

**Services implemented:** POST, GET All, GET By ID, PUT, DELETE

---

## Employee Fields

The same Employee fields from Assignment 1 are used.

---

## 2a. Create Employee

### Endpoint

```http
POST /api/EmployeeDapper
```

### Test Data

| Field | Value |
|---|---|
| First Name | Anjali |
| Last Name | Menon |
| Date of Birth | 1995-04-12 |
| Personal Email | anjali.menon@example.net |
| Mobile Number | +919846012345 |
| Postal Address | 42B, Skyline Apartments, Kakkanad |
| Gender | 1 – Female |
| Country | India |
| City | Kochi |
| Designation | Senior Software Engineer |
| Basic Pay | 85000.00 |
| Need Transportation | true |
| Notes | Prefers office cab pickup from Kakkanad junction. |
| Username | anjali_m |
| Password | `<your-password>` |

### Request Body

```json
{
  "employeeId": 0,
  "firstName": "Anjali",
  "lastName": "Menon",
  "dateOfBirth": "1995-04-12T00:00:00",
  "personalEmail": "anjali.menon@example.net",
  "mobileNumber": "+919846012345",
  "postalAddress": "42B, Skyline Apartments, Kakkanad",
  "gender": 1,
  "country": "India",
  "city": "Kochi",
  "designation": "Senior Software Engineer",
  "basicPay": 85000.00,
  "needTransportation": true,
  "notes": "Prefers office cab pickup from Kakkanad junction.",
  "username": "anjali_m",
  "password": "<your-password>"
}
```

### Expected Output

```json
true
```

---

## 2b. Get All Employees

### Endpoint

```http
GET /api/EmployeeDapper
```

### Request Body

None.

### Expected Output

Returns all employees from the database.

### Response Body

```json
[
  {
    "employeeId": 11,
    "firstName": "Natasha",
    "lastName": "Romanoff",
    "dateOfBirth": "1994-11-22T00:00:00",
    "personalEmail": "natasha.romanoff@example.com",
    "mobileNumber": "+12025550143",
    "postalAddress": "742 Evergreen Terrace, Arlington",
    "gender": 1,
    "country": "United States of America",
    "city": "Arlington",
    "designation": "Software Engineer",
    "basicPay": 125000.00,
    "needTransportation": true,
    "notes": "Requires secure remote network access.",
    "username": "natromanoff",
    "password": "<your-password>"
  },
  {
    "employeeId": 12,
    "firstName": "Anjali",
    "lastName": "Menon",
    "dateOfBirth": "1995-04-12T00:00:00",
    "personalEmail": "anjali.menon@example.net",
    "mobileNumber": "+919846012345",
    "postalAddress": "42B, Skyline Apartments, Kakkanad",
    "gender": 1,
    "country": "India",
    "city": "Kochi",
    "designation": "Senior Software Engineer",
    "basicPay": 85000.00,
    "needTransportation": true,
    "notes": "Prefers office cab pickup from Kakkanad junction.",
    "username": "anjali_m",
    "password": "<your-password>"
  }
]
```

---

## 2c. Get Employee By ID

### Endpoint

```http
GET /api/EmployeeDapper/12
```

### Test Data

**Employee ID:** `12`

### Request Body

None.

### Expected Output

Returns the employee with ID `12`, or `404 Not Found` if the employee does not exist.

### Response Body

```json
{
  "employeeId": 12,
  "firstName": "Anjali",
  "lastName": "Menon",
  "dateOfBirth": "1995-04-12T00:00:00",
  "personalEmail": "anjali.menon@example.net",
  "mobileNumber": "+919846012345",
  "postalAddress": "42B, Skyline Apartments, Kakkanad",
  "gender": 1,
  "country": "India",
  "city": "Kochi",
  "designation": "Senior Software Engineer",
  "basicPay": 85000.00,
  "needTransportation": true,
  "notes": "Prefers office cab pickup from Kakkanad junction.",
  "username": "anjali_m",
  "password": "<your-password>"
}
```

---

## 2d. Update Employee

### Endpoint

```http
PUT /api/EmployeeDapper/12
```

### Test Data

- Employee ID: `12`
- Designation: `Project Manager`
- Basic Pay: `135000.00`
- Need Transportation: `true`

### Request Body

```json
{
  "employeeId": 12,
  "firstName": "Anjali",
  "lastName": "Menon",
  "dateOfBirth": "1995-04-12T00:00:00",
  "personalEmail": "anjali.menon@example.net",
  "mobileNumber": "+919846012345",
  "postalAddress": "42B, Skyline Apartments, Kakkanad",
  "gender": 1,
  "country": "India",
  "city": "Kochi",
  "designation": "Project Manager",
  "basicPay": 135000.00,
  "needTransportation": true,
  "notes": "Promoted to Project Manager.",
  "username": "anjali_m",
  "password": "<your-password>"
}
```

### Expected Output

```json
true
```

---

## 2e. Delete Employee

### Endpoint

```http
DELETE /api/EmployeeDapper/12
```

### Test Data

**Employee ID:** `12`

### Request Body

None.

### Expected Output

```json
true
```

### Not Found

```http
DELETE /api/EmployeeDapper/99
```

Returns HTTP `404 Not Found`.

---

# Assignment 3: Employee + WorkExperience CRUD using Entity Framework Core

## Objective

Extend the Employee CRUD service to support a second related table, **WorkExperience**, using Entity Framework Core.

The relationship is:

```text
Employee
   │
   │ EmployeeId
   │
   ├── WorkExperience
   ├── WorkExperience
   └── WorkExperience
```

One employee can have multiple work-experience records.

**Controller:** `Employee2Controller`

**Technology:** Entity Framework Core

**Services implemented:** POST, GET All, GET By ID, PUT, DELETE

---

## Employee Fields

The Employee fields from Assignment 1 are used.

## WorkExperience Fields

- WorkExperienceId
- EmployeeId
- Company
- NumberOfMonths
- LastDesignation
- Remarks

---

## 3a. Create Employee with WorkExperience

### Endpoint

```http
POST /api/Employee2
```

### Request Body

```json
{
  "employeeId": 0,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Senior Software Engineer",
  "basicPay": 80000.00,
  "needTransportation": false,
  "notes": "Relocating from the east coast branch.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 0,
      "employeeId": 0,
      "company": "Innovate LLC",
      "numberOfMonths": 18,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    }
  ]
}
```

### Expected Output

```json
true
```

> `employeeId` and `workExperienceId` can be `0` when they are generated by the database/application.

---

## 3b. Get All Employees with WorkExperience

### Endpoint

```http
GET /api/Employee2
```

### Request Body

None.

### Expected Output

Returns all employees together with their associated work experiences.

### Response Body

```json
[
  {
    "employeeId": 15,
    "firstName": "Jonathan",
    "lastName": "Doe",
    "dateOfBirth": "1994-05-15T00:00:00",
    "personalEmail": "johndoe@example.com",
    "mobileNumber": "+15551234567",
    "postalAddress": "123 Innovation Way, Suite 400",
    "gender": 2,
    "country": "United Kingdom",
    "city": "Manchester",
    "designation": "Senior Software Engineer",
    "basicPay": 80000.00,
    "needTransportation": false,
    "notes": "Relocating from the east coast branch.",
    "username": "johndoe94",
    "password": "<your-password>",
    "workExperiences": [
      {
        "workExperienceId": 3,
        "employeeId": 15,
        "company": "Innovate LLC",
        "numberOfMonths": 18,
        "lastDesignation": "Junior Developer",
        "remarks": "Completed internship and converted to full-time role."
      }
    ]
  }
]
```

---

## 3c. Get Employee By ID

### Endpoint

```http
GET /api/Employee2/15
```

### Test Data

**Employee ID:** `15`

### Request Body

None.

### Response Body

```json
{
  "employeeId": 15,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Senior Software Engineer",
  "basicPay": 80000.00,
  "needTransportation": false,
  "notes": "Relocating from the east coast branch.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 3,
      "employeeId": 15,
      "company": "Innovate LLC",
      "numberOfMonths": 18,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    }
  ]
}
```

### Not Found

```http
GET /api/Employee2/99
```

Returns HTTP `404 Not Found`.

---

## 3d. Update Employee and WorkExperience

### Endpoint

```http
PUT /api/Employee2/15
```

### Test Data

- Employee ID: `15`
- Designation: `Senior Software Engineer`
- Basic Pay: `80000.00`
- Need Transportation: `false`

### Request Body

```json
{
  "employeeId": 15,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Senior Software Engineer",
  "basicPay": 80000.00,
  "needTransportation": false,
  "notes": "Relocating from the east coast branch.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 3,
      "employeeId": 15,
      "company": "Innovate LLC",
      "numberOfMonths": 25,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    }
  ]
}
```

### Expected Output

```json
true
```

---

## 3e. Delete Employee and WorkExperience

### Endpoint

```http
DELETE /api/Employee2/15
```

### Test Data

**Employee ID:** `15`

### Request Body

None.

### Expected Output

```json
true
```

### Not Found

```http
DELETE /api/Employee2/99
```

Returns HTTP `404 Not Found`.

---

# Assignment 4: Employee + WorkExperience CRUD using Dapper and SQL Queries

## Objective

Create an Employee Management service using **Dapper and SQL queries** for multiple related database tables.

The service supports:

- Add Employee with WorkExperience
- Get All Employees with WorkExperience
- Get Employee By ID with WorkExperience
- Update Employee and WorkExperience
- Delete Employee and WorkExperience

**Controller:** `EmployeeDapper2Controller`

**Technology:** Dapper + SQL Queries

**Services implemented:** POST, GET All, GET By ID, PUT, DELETE

---

## Employee Fields

The Employee fields from Assignment 1 are used.

## WorkExperience Fields

- WorkExperienceId
- EmployeeId
- Company
- NumberOfMonths
- LastDesignation
- Remarks

---

## 4a. Create Employee with Multiple WorkExperience Records

### Endpoint

```http
POST /api/EmployeeDapper2
```

### Request Body

```json
{
  "employeeId": 0,
  "firstName": "Leon",
  "lastName": "Kennedy",
  "dateOfBirth": "1995-11-22T00:00:00",
  "personalEmail": "leon.kennedy@example.net",
  "mobileNumber": "+919876543210",
  "postalAddress": "789 Raccoon City, Apt 12B",
  "gender": 2,
  "country": "United States of America",
  "city": "Los Santos",
  "designation": "Team Leader",
  "basicPay": 110000.00,
  "needTransportation": true,
  "notes": "Requires company transportation.",
  "username": "kennedy_leon",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 0,
      "employeeId": 0,
      "company": "Umbrella Corporation",
      "numberOfMonths": 34,
      "lastDesignation": "Team Lead",
      "remarks": "Left on good terms for career growth opportunities."
    },
    {
      "workExperienceId": 0,
      "employeeId": 0,
      "company": "Tricell",
      "numberOfMonths": 42,
      "lastDesignation": "Team Lead",
      "remarks": "Left on good terms for career growth opportunities."
    }
  ]
}
```

### Expected Output

```json
true
```

> The application/database generates the Employee ID and WorkExperience IDs.

---

## 4b. Get All Employees with WorkExperience

### Endpoint

```http
GET /api/EmployeeDapper2
```

### Request Body

None.

### Expected Output

Returns all employees and their associated work-experience records.

### Response Body

```json
[
  {
    "employeeId": 15,
    "firstName": "Jonathan",
    "lastName": "Doe",
    "dateOfBirth": "1994-05-15T00:00:00",
    "personalEmail": "johndoe@example.com",
    "mobileNumber": "+15551234567",
    "postalAddress": "123 Innovation Way, Suite 400",
    "gender": 2,
    "country": "United Kingdom",
    "city": "Manchester",
    "designation": "Senior Software Engineer",
    "basicPay": 80000.00,
    "needTransportation": false,
    "notes": "Relocating from the east coast branch.",
    "username": "johndoe94",
    "password": "<your-password>",
    "workExperiences": [
      {
        "workExperienceId": 3,
        "employeeId": 15,
        "company": "Innovate LLC",
        "numberOfMonths": 18,
        "lastDesignation": "Junior Developer",
        "remarks": "Completed internship and converted to full-time role."
      }
    ]
  },
  {
    "employeeId": 16,
    "firstName": "Leon",
    "lastName": "Kennedy",
    "dateOfBirth": "1995-11-22T00:00:00",
    "personalEmail": "leon.kennedy@example.net",
    "mobileNumber": "+919876543210",
    "postalAddress": "789 Raccoon City, Apt 12B",
    "gender": 2,
    "country": "United States of America",
    "city": "Los Santos",
    "designation": "Team Leader",
    "basicPay": 110000.00,
    "needTransportation": true,
    "notes": "Requires company transportation.",
    "username": "kennedy_leon",
    "password": "<your-password>",
    "workExperiences": [
      {
        "workExperienceId": 4,
        "employeeId": 16,
        "company": "Umbrella Corporation",
        "numberOfMonths": 34,
        "lastDesignation": "Team Lead",
        "remarks": "Left on good terms for career growth opportunities."
      },
      {
        "workExperienceId": 5,
        "employeeId": 16,
        "company": "Tricell",
        "numberOfMonths": 42,
        "lastDesignation": "Team Lead",
        "remarks": "Left on good terms for career growth opportunities."
      }
    ]
  }
]
```

---

## 4c. Get Employee By ID

### Endpoint

```http
GET /api/EmployeeDapper2/15
```

### Test Data

**Employee ID:** `15`

### Request Body

None.

### Response Body

```json
{
  "employeeId": 15,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Senior Software Engineer",
  "basicPay": 80000.00,
  "needTransportation": false,
  "notes": "Relocating from the east coast branch.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 3,
      "employeeId": 15,
      "company": "Innovate LLC",
      "numberOfMonths": 25,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    }
  ]
}
```

### Not Found

```http
GET /api/EmployeeDapper2/99
```

Returns HTTP `404 Not Found`.

---

## 4d. Update Employee and WorkExperience

### Endpoint

```http
PUT /api/EmployeeDapper2/15
```

### Test Data

- Employee ID: `15`
- Designation: `Project Manager`
- Basic Pay: `135000.00`
- Need Transportation: `true`

### Request Body

```json
{
  "employeeId": 15,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Project Manager",
  "basicPay": 135000.00,
  "needTransportation": true,
  "notes": "Promoted to Project Manager.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 3,
      "employeeId": 15,
      "company": "Innovate LLC",
      "numberOfMonths": 25,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    },
    {
      "workExperienceId": 6,
      "employeeId": 15,
      "company": "Aperture Science",
      "numberOfMonths": 8,
      "lastDesignation": "Technical Support Specialist",
      "remarks": "Left during the probationary period."
    }
  ]
}
```

### Expected Output

```json
true
```

---

## 4e. Delete Employee and WorkExperience

### Endpoint

```http
DELETE /api/EmployeeDapper2/15
```

### Test Data

**Employee ID:** `15`

### Request Body

None.

### Expected Output

```json
true
```

### Not Found

```http
DELETE /api/EmployeeDapper2/99
```

Returns HTTP `404 Not Found`.

---

# Assignment 5: Employee + WorkExperience CRUD using Dapper and SQL StoredProcedure

## Objective

Create an Employee Management service using **Dapper and SQL StoredProcedure** for multiple related database tables.

The service supports:

- Add Employee with WorkExperience(using StoredProcedure)
- Update Employee and WorkExperience(using StoredProcedure)
- Delete Employee and WorkExperience(using StoredProcedure)

**Controller:** `EmployeeDapper3Controller`

**Technology:** Dapper + SQL StoredProcedure

**Services implemented:** POST,PUT, DELETE

---

## Employee Fields

The Employee fields from Assignment 1 are used.

## WorkExperience Fields

- WorkExperienceId
- EmployeeId
- Company
- NumberOfMonths
- LastDesignation
- Remarks

---

## 5a. Create Employee with Multiple WorkExperience Records

### Endpoint

```http
POST /api/EmployeeDapper3
```

### Request Body

```json
{
  "employeeId": 0,
  "firstName": "S",
  "lastName": "Ky",
  "dateofBirth": "1995-11-22T00:00:00",
  "personalEmail": "sky@example.net",
  "mobileNumber": "7568924356",
  "postalAddress": "789 Racoon City, Apt 12B",
  "gender": 2,
  "country": "United States of America",
  "city": "Los Santos",
  "designation": "Team Leader",
  "basicPay": 110000,
  "needTransportation": true,
  "notes": "Does'nt require parking pass validation if company transit isn't used.",
  "username": "SKy",
  "password": "RacoonsedededDpt",
  "workExperiences": [
    {
      "workExperienceId": 0,
      "employeeId": 0,
      "company": "Umbrella Corporation",
      "numberOfMonths": 34,
      "lastDesignation": "Team Lead",
      "remarks": "Left on good terms for career growth opportunities."
    },
    {
      "workExperienceId": 0,
      "employeeId": 0,
      "company": "Tricell",
      "numberOfMonths": 42,
      "lastDesignation": "Team Lead",
      "remarks": "Left on good terms for career growth opportunities."
    }
  ]
}
```

### Expected Output

```json
true
```

> The application/database generates the Employee ID and WorkExperience IDs.

---

## 5b. Update Employee and WorkExperience

### Endpoint

```http
PUT /api/EmployeeDapper3/15
```

### Test Data

- Employee ID: `15`
- Designation: `Project Manager`
- Basic Pay: `135000.00`
- Need Transportation: `true`

### Request Body

```json
{
  "employeeId": 15,
  "firstName": "Jonathan",
  "lastName": "Doe",
  "dateOfBirth": "1994-05-15T00:00:00",
  "personalEmail": "johndoe@example.com",
  "mobileNumber": "+15551234567",
  "postalAddress": "123 Innovation Way, Suite 400",
  "gender": 2,
  "country": "United Kingdom",
  "city": "Manchester",
  "designation": "Project Manager",
  "basicPay": 135000.00,
  "needTransportation": true,
  "notes": "Promoted to Project Manager.",
  "username": "johndoe94",
  "password": "<your-password>",
  "workExperiences": [
    {
      "workExperienceId": 3,
      "employeeId": 15,
      "company": "Innovate LLC",
      "numberOfMonths": 25,
      "lastDesignation": "Junior Developer",
      "remarks": "Completed internship and converted to full-time role."
    },
    {
      "workExperienceId": 6,
      "employeeId": 15,
      "company": "Aperture Science",
      "numberOfMonths": 8,
      "lastDesignation": "Technical Support Specialist",
      "remarks": "Left during the probationary period."
    }
  ]
}
```

### Expected Output

```json
true
```

---

## 5c. Delete Employee and WorkExperience

### Endpoint

```http
DELETE /api/EmployeeDapper3/31
```

### Test Data

**Employee ID:** `31`

### Request Body

None.

### Expected Output

```json
true
```

### Not Found

```http
DELETE /api/EmployeeDapper3/99
```

Returns HTTP `404 Not Found`.

---

# Assignment 6: Paging & Sorting using EF

## Objective

Create an Employee Management service Implement paging and sorting for Employee List using **EF and SQL database**  database tables.

The service supports:

- GET Employees page by page.
- Specify the no.of employess display per page.
- Sort employees by a selected field.
- Sort employees in ascending or descending order.


**Controller:** `EmployeePageSortingController`

**Technology:** Entity Framework Core, SQL Server

**Services implemented:** GET

---

## 6.  Paging & Sorting using EF

### Endpoint

```http
GET /api/EmployeePageSorting/Paged/5/1/firstname/asc
```

### Request Body

None.

### Expected Output

Returns first 5 employees sorted in ascending order based on their username .

### Response Body

```json
[
  {
    "employeeId": 35,
    "firstName": "Aditi",
    "lastName": "Sharma",
    "dateofBirth": "1994-04-12T00:00:00",
    "personalEmail": "aditi.sharma94@gmail.com",
    "mobileNumber": "+91-9812233445",
    "postalAddress": "12, Sector 21, Faridabad, Haryana, India",
    "gender": 1,
    "country": "India",
    "city": "Faridabad",
    "designation": "Software Engineer",
    "basicPay": 55000,
    "needTransportation": true,
    "notes": "Recent hire, working on inventory module.",
    "username": "aditi.sharma",
    "password": "Aditi@Pass01",
    "workExperiences": []
  },
  {
    "employeeId": 13,
    "firstName": "Anjali",
    "lastName": "Menon",
    "dateofBirth": "1995-04-12T00:00:00",
    "personalEmail": "anjali.menon@example.net",
    "mobileNumber": "+919846012345",
    "postalAddress": "42B, Skyline Apartments, Kakkanad",
    "gender": 1,
    "country": "India",
    "city": "Kochi",
    "designation": "Senior Software Engineer",
    "basicPay": 85000,
    "needTransportation": true,
    "notes": "Prefers office cab pickup from Kakkanad junction.",
    "username": "anjali_m",
    "password": "SecurePass!2026",
    "workExperiences": []
  },
  {
    "employeeId": 46,
    "firstName": "Chris",
    "lastName": "Evans",
    "dateofBirth": "1986-11-11T00:00:00",
    "personalEmail": "chris.evans@hotmail.com",
    "mobileNumber": "+1-4045550123",
    "postalAddress": "250 Peachtree St, Atlanta, GA",
    "gender": 2,
    "country": "United States of America",
    "city": "Atlanta",
    "designation": "Team Leader",
    "basicPay": 97500,
    "needTransportation": false,
    "notes": "Leads QA automation for East Coast clients.",
    "username": "chris.evans",
    "password": "Chris@Atlanta11",
    "workExperiences": []
  },
  {
    "employeeId": 43,
    "firstName": "Divya",
    "lastName": "Krishnan",
    "dateofBirth": "1995-10-16T00:00:00",
    "personalEmail": "divya.krishnan95@gmail.com",
    "mobileNumber": "+91-9445566778",
    "postalAddress": "22, Anna Salai, Chennai, Tamil Nadu, India",
    "gender": 1,
    "country": "India",
    "city": "Chennai",
    "designation": "Team Leader",
    "basicPay": 93000,
    "needTransportation": true,
    "notes": "Leads UI/UX standardization initiative.",
    "username": "divya.krishnan",
    "password": "Divya@Secure16",
    "workExperiences": []
  },
  {
    "employeeId": 44,
    "firstName": "Ethan",
    "lastName": "Walker",
    "dateofBirth": "1991-07-04T00:00:00",
    "personalEmail": "ethan.walker@yahoo.com",
    "mobileNumber": "+44-7822334455",
    "postalAddress": "30 Royal Mile, Edinburgh, United Kingdom",
    "gender": 2,
    "country": "United Kingdom",
    "city": "Edinburgh",
    "designation": "Project Manager",
    "basicPay": 132000,
    "needTransportation": false,
    "notes": "Manages Scotland regional client accounts.",
    "username": "ethan.walker",
    "password": "Ethan@PM2024",
    "workExperiences": []
  }
]
```
---
# Assignment 7: Paging & Sorting using Drapper

## Objective

Create an Employee Management service Implement paging and sorting for Employee List using **Dapper and SQL Query**  database tables.

The service supports:

- GET Employees page by page.
- Specify the no.of employess display per page.
- Sort employees by a selected field.
- Sort employees in ascending or descending order.


**Controller:** `EmployeePageSortingController`

**Technology:** Entity Framework Core, SQL Server

**Services implemented:** GET

---
## 7. Implement paging and sorting for Employee List Service using Dapper Framework

### Endpoint

```http
GET /api/EmployeePageSortingDapper/Paged/5/2/dateofbirth/desc
```

### Request Body

None.

### Expected Output

Returns 5 employees sorted in ascending order based on their username after skipping first 5 employees.

### Response Body

```json
[
  {
    "employeeId": 13,
    "firstName": "Anjali",
    "lastName": "Menon",
    "dateofBirth": "1995-04-12T00:00:00",
    "personalEmail": "anjali.menon@example.net",
    "mobileNumber": "+919846012345",
    "postalAddress": "42B, Skyline Apartments, Kakkanad",
    "gender": 1,
    "country": "India",
    "city": "Kochi",
    "designation": "Senior Software Engineer",
    "basicPay": 85000,
    "needTransportation": true,
    "notes": "Prefers office cab pickup from Kakkanad junction.",
    "username": "anjali_m",
    "password": "SecurePass!2026",
    "workExperiences": []
  },
  {
    "employeeId": 12,
    "firstName": "Natasha",
    "lastName": "Romanoff",
    "dateofBirth": "1994-11-22T00:00:00",
    "personalEmail": "blackwidow@shield.gov",
    "mobileNumber": "+12025550143",
    "postalAddress": "742 Evergreen Terrace, Washington DC",
    "gender": 1,
    "country": "United States of America",
    "city": "Arlington",
    "designation": "Software Engineer",
    "basicPay": 125000,
    "needTransportation": true,
    "notes": "Requires secure remote network access.",
    "username": "natromanoff",
    "password": "SecurePassword!99",
    "workExperiences": []
  },
  {
    "employeeId": 47,
    "firstName": "Radhika",
    "lastName": "Bhatt",
    "dateofBirth": "1994-09-09T00:00:00",
    "personalEmail": "radhika.bhatt94@yahoo.com",
    "mobileNumber": "+91-9123344556",
    "postalAddress": "17, Vastrapur, Ahmedabad, Gujarat, India",
    "gender": 1,
    "country": "India",
    "city": "Ahmedabad",
    "designation": "Project Leader",
    "basicPay": 99000,
    "needTransportation": true,
    "notes": "Coordinates offshore delivery for EU clients.",
    "username": "radhika.bhatt",
    "password": "Radhika@Secure09",
    "workExperiences": []
  },
  {
    "employeeId": 15,
    "firstName": "Jonathan",
    "lastName": "Doe",
    "dateofBirth": "1994-05-15T00:00:00",
    "personalEmail": "johndoe@example.com",
    "mobileNumber": "+15551234567",
    "postalAddress": "123 Innovation Way, Suite 400",
    "gender": 2,
    "country": "United Kingdom",
    "city": "Manchester",
    "designation": "Senior Software Engineer",
    "basicPay": 80000,
    "needTransportation": false,
    "notes": "Relocating from the east coast branch. Prefers remote onboarding.",
    "username": "johndoe94",
    "password": "SecurePassword123!",
    "workExperiences": []
  },
  {
    "employeeId": 35,
    "firstName": "Aditi",
    "lastName": "Sharma",
    "dateofBirth": "1994-04-12T00:00:00",
    "personalEmail": "aditi.sharma94@gmail.com",
    "mobileNumber": "+91-9812233445",
    "postalAddress": "12, Sector 21, Faridabad, Haryana, India",
    "gender": 1,
    "country": "India",
    "city": "Faridabad",
    "designation": "Software Engineer",
    "basicPay": 55000,
    "needTransportation": true,
    "notes": "Recent hire, working on inventory module.",
    "username": "aditi.sharma",
    "password": "Aditi@Pass01",
    "workExperiences": []
  }
]
```
---

## Assignment 8: Service Class Implementation using Dapper

## Objective 

Implement Employee Service Class as described in Video 21 using Entity Framework. Make necessary changes in Employee Controller.

### 8a–8e. Post / Get All / Get By Id / Put / Delete

Same request/response payload shapes and test values as Assignment 1 (EF), but routed through `EmployeeServiceController`.

---

## Assignment 9: Service Class Implementation using Dapper

## Objective
Implement Employee Service Class as described in Video 21 using Dapper Framework. The signature of each public method should be same as the service created using Entity Framework. There shouldn't be any changes needed in controller class except name space inclusion.


### 9a–9e. Post / Get All / Get By Id / Put / Delete

Same request/response payload shapes and test values as Assignment 2 (Dapper), but routed through `EmployeeServiceDapperController`.

---

CREATE TABLE Project
(
    ProjectId INT IDENTITY(1,1) PRIMARY KEY,

    Name NVARCHAR(200) NOT NULL,

    Description NVARCHAR(MAX) NOT NULL,

    ProjectManagerId INT NOT NULL,

    StartDate DATETIME NOT NULL,

    TentativeClosingDate DATETIME NULL,

    AccountManagerId INT NOT NULL,

    CONSTRAINT FK_Project_ProjectManager
        FOREIGN KEY (ProjectManagerId)
        REFERENCES EmployeeDetails(EmployeeId),

    CONSTRAINT FK_Project_AccountManager
        FOREIGN KEY (AccountManagerId)
        REFERENCES EmployeeDetails(EmployeeId),

    CONSTRAINT CHK_Project_Dates
        CHECK
        (
            TentativeClosingDate IS NULL
            OR TentativeClosingDate >= StartDate
        )
);#   E R P A P I  
 