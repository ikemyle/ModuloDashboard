This is a WPF template application that works with modularity and roled based authentication.
How do we add modules to the app:
- Create a new module project. It must contain a class that inherits from Prism.Modularity.IModule
- See example modules under Modules (DM.ModuleOne and DM.ModuleTwo)
- Users, Roles, Modules and associated User-Roles, Modules-Roles come from an SQL Server database (script included here in main project ../SQL/DBTables.sql)
- The default user, roles and modules are created from the code
- ORM used is Linq2DB
- All nuget packages are up to date as the time of the repository was created
- users admin/admin, guest/guest

  as admin
  ![image](https://github.com/ikemyle/ModuloDashboard/assets/975391/af849966-4f62-403b-8771-a147f8772f07)
  
![image](https://github.com/ikemyle/ModuloDashboard/assets/975391/ba730a43-0d37-453c-aed5-ff7880772c9b)


![image](https://github.com/ikemyle/ModuloDashboard/assets/975391/5d4e64fe-3c09-424f-9861-86c84e661b08)

![image](https://github.com/ikemyle/ModuloDashboard/assets/975391/4e604165-3cb0-4cba-8506-fde6d072a05e)

as guest

![image](https://github.com/ikemyle/ModuloDashboard/assets/975391/295fac90-8ebe-4a20-add5-30d00a1995c8)
