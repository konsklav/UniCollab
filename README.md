# Υπηρεσιοστρεφές Λογισμικό
- Γιώργος Νικολαΐδης (**p21115**)
- Κωνσταντίνος Σκλαβενίτης (**p21XXX**)
 
# Requirements
- [Docker](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js](https://nodejs.org/en)

# Setup
### 1. Running the Web API
Τα modules του back-end (Web API, Database, WebSockets) είναι ρυθμισμένα να τρέχουν ως containers. Το **compose.yaml** που βρίσκεται στο api/Saas.Api folder φροντίζει να τρέχουν όλα τα containers μαζί.  
``` bash
cd ./apps/api/Saas.Api
docker-compose up --build
```

### 2. Running the website
Το React site το τρέχουμε ξεχωριστά από το back-end stack. Αυτό επειδή θέλουμε στο React development να έχουμε hot reload (HMR), κάτι που δεν είναι εφικτό αν κάνουμε containerize το React app.  
```bash
cd ./apps/website
npm install vite
npm install
npm run dev
```
Για να τρέξουμε όλο το stack (front και back-end) εκτελούμε τα παραπάνω βήματα.
