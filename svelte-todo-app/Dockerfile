FROM node:latest

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY ./src ./src
COPY ./public ./public

EXPOSE 6000

ENV HOST=0.0.0.0

RUN npm run dev