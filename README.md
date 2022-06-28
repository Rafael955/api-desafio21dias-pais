# Comandos iniciais:
``` bash
  mkdir webapi-pais
  cd webapi-pai
  dotnet new webapi
```

# Comandos git:
``` bash
  code .gitignore 
```
### gerei o conteúdo para ignorar como (Windows, Linux, Mac, DotnetCore, VisualStudioCore) no link: https://www.toptal.com/developers/gitignore
- Criei o repositório e rodei os comandos

``` bash
  git init
  git add .
  git commit -m "Iniciando projeto"
  git remote add origin git@github.com:Rafael955/aula-desafio-21dias-pais.git
  git branch -M main
  git push -u origin main
```

# Componentes instalados:
``` bash
  dotnet add package Swashbuckle.AspNetCore --version 5.6.3
  dotnet add package mongocsharpdriver --version 2.15.0
  dotnet add package Newtonsoft.Json --version 13.0.1
```