
                    cd 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services'
                    mkdir Ordering
                    cd 'Ordering'
                    dotnet new webapi -n Ordering.API
                    dotnet new classlib -n Ordering.Application
                    dotnet new classlib -n Ordering.Domain
                    dotnet new classlib -n Ordering.Infrastructure
                    cd 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src'
                    dotnet sln ecommerce-microservices.sln add 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services/Ordering/Ordering.API/Ordering.API.csproj'
                    dotnet sln ecommerce-microservices.sln add 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services/Ordering/Ordering.Application/Ordering.Application.csproj'
                    dotnet sln ecommerce-microservices.sln add 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services/Ordering/Ordering.Domain/Ordering.Domain.csproj'
                    dotnet sln ecommerce-microservices.sln add 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj'
                    cd 'D:\Programownie\Kursy\Microservices Mehmet Ozkaya\MyApp\src\Services\Ordering'
                    cd Ordering.API
                    dotnet add reference ..\Ordering.Application\Ordering.Application.csproj
                    cd Ordering.Application
                    dotnet add reference ..\Ordering.Domain\Ordering.Domain.csproj
                    dotnet add reference ..\Ordering.Infrastructure\Ordering.Infrastructure.csproj
                    cd Ordering.Infrastructure
                    dotnet add reference ..\Ordering.Domain\Ordering.Domain.csproj
                    cd ..
                    dotnet restore
