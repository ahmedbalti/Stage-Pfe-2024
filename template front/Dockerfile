# Stage 1: Build the Angular app
FROM node:18 AS build
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application source code
COPY . .

# Build the Angular application
RUN npm run build --prod

# Stage 2: Serve the app using nginx
FROM nginx:alpine
COPY --from=build /app/dist/template front/usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start nginx server
CMD ["nginx", "-g", "daemon off;"]
