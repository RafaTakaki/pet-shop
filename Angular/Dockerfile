FROM node:20-alpine AS build
WORKDIR /app

# Instala globalmente primeiro (backup)
RUN npm install -g @angular/cli@latest

COPY package*.json ./
RUN npm ci

COPY . .
# Usa ng global OU npx
RUN npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/desafio-teste/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]