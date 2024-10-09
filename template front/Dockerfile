# Étape 1 : Construire l'application Angular
FROM node:18-alpine AS build
WORKDIR /app

# Copier les fichiers package.json et package-lock.json pour installer les dépendances
COPY package*.json ./
RUN npm install --force

# Copier le reste du code source et construire l'application
COPY . .
RUN npm run build -- --configuration production

# Étape 2 : Servir l'application avec NGINX
FROM nginx:alpine
COPY --from=build /app/dist/argon-design-system-angular /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]