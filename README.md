# Sistemas Distribuídos (Unified Health System)

<p align="center"><img src="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/img/logo.png" width="250px"></p>

Este repositório trata-se de um trabalho de **Sistemas Distribuídos (SDIC7)** do **Instituto Federal de Ciências e Tecnologia de São Paulo (IFSP) - Campus Piracicaba**. Este grupo composto pelos integrantes desenvolvedores Antonio Naranjo Cerda, Daniel Galdencio dos Santos, Isabelle Caroline de Carvalho Costa, João Pedro Carpanezi dos Santos e Murilo Azevedo Jacon. Este projeto é uma versão alternativa do projeto <a href="https://github.com/jpcarpanezi/dooc-health-system">dooc-health-system</a>, e implementa uma arquitetura descentralizada de microserviços, que compõe um conjunto de APIs RESTful com a proposta de um sistema unificado de saúde para todo o país, permitindo que em qualquer cidade que a pessoa visite um hospital todo seu histórico médico possa ser resgatado.

## Instalação
Caso necessário, uma coleção do Postman com todas as requisições para os endpoints das APIs está disponível <a href="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/postman_collection.json">clicando aqui</a>.

### Utilizando Docker
**1º)** Clone o repositório
```
git clone https://github.com/jpcarpanezi/sdic7-health-system.git
```

**2º)** Execute o arquivo YAML do Docker Compose<br>
**Atenção:** Caso as portas 8081 ou 8082 estejam em uso será necessário alterar as portas no arquivo YAML, ou liberar para que as requisições sejam feitas.
```
docker-compose up -d
```

### Utilizando Kubernetes (k8s)
**1º)** Clone o repositório
```
git clone https://github.com/jpcarpanezi/sdic7-health-system.git
```

**2º)** Execute o cluster do k8s
```
minikube start
```

**3º)** Execute todos os YAML das configurações
```
kubectl apply -f /path/to/repo/deploy/k8s/ --recursive
```

**4º)** Realize o port-forward
```
kubectl port-forward service/medicines 8081:80
```

```
kubectl port-forward service/person 8082:80
```

```
kubectl port-forward service/mysql 3306:3306
```

**5º)** Realizar importação do banco de dados<br>

Exclusivamente para deploys realizados em k8s, deve ser acessado o banco de dados MySQL e realizada a importação dos arquivos <a href="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/deploy/databases/health-medicines.sql">health-medicines.sql</a> e <a href="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/deploy/databases/health-medicines.sql">health-person.sql</a>

## Overview da arquitetura
Temos como referência na imagem, um sistema descentralizado de APIs em microserviços, desenvolvido .NET 6 com armazenamento de dados no MySQL, que se comunicam entre sí de forma assíncrona utilizando uma fila de mensagens do RabbitMQ. Desta forma, uma outra aplicação front-end poderá consumir atráves do protocolo HTTP.
<p align="center"><img src="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/img/arquitetura.png"></p>

## Build status (GitHub Actions)
| Image | Status |
| ------------- | ------------- |
| Person API | [![Person.API](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/person-api.yml/badge.svg?branch=master)](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/person-api.yml) |
| Medicines API | [![Medicines.API](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/medicines-api.yml/badge.svg?branch=master)](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/medicines-api.yml) |
| Ocelot Gateway | [![Ocelot.Gateway](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/ocelot-gateway.yml/badge.svg?branch=master)](https://github.com/jpcarpanezi/sdic7-health-system/actions/workflows/ocelot-gateway.yml) |

## Licença
Este projeto é um trabalho de cunho acadêmico, voltado para conhecimento de estrutura de dados e sem fins lucrativos. Olhe o arquivo de <a href="https://github.com/jpcarpanezi/sdic7-health-system/blob/master/LICENSE.md" target="_blank">LICENSE</a> para direitos e limitações (MIT).
