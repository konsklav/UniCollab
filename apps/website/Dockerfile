# STEP 1 - Compile and build the React App
FROM node:23-alpine3.21 AS build 
WORKDIR /app
COPY package.json .
RUN npm install
RUN npm install --global serve
COPY . . 
RUN npm run build
EXPOSE 3000
CMD [ "serve", "-s", "dist"]