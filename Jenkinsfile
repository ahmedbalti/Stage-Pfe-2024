pipeline {
    agent any
    tools {
        dotnetsdk 'dotnet-sdk-6.0'
    }

    environment {
        SONARQUBE_KEY = 'squ_9d5e5b0c3916633347cdbdc85ec4bb3e54e89c5e'
        PROJECT_NAME = 'projet_pfe'
        SONARQUBE_URL = 'http://192.168.33.10:9000'
    }
    
    stages {
        stage('Restore Dependencies') {
            steps {
                dir('BackEnd/Gestion User') {
                    script {
                        sh 'dotnet restore'
                    }
                }
            }
        }

        stage('Build') {
            steps {
                dir('BackEnd/Gestion User') {
                    script {
                        sh 'dotnet build --configuration Release'
                    }
                }
            }
        }

        stage('Run Tests') {
            steps {
                dir('BackEnd/Gestion User') {
                    script {
                        sh 'dotnet test'
                    }
                }
            }
        }

  stage('Code Analysis with SonarQube') {
    steps {
        dir('BackEnd/Gestion User') {
            script {
                sh """
                dotnet sonarscanner begin /k:"${PROJECT_NAME}" /d:sonar.host.url=${SONARQUBE_URL} /d:sonar.login=${SONARQUBE_KEY}
                dotnet build --no-incremental /warnaserror- /nowarn:CS8618,CS8603,CS8604,CS8602
                dotnet sonarscanner end /d:sonar.login=${SONARQUBE_KEY}
                """
            }
        }
    }
}
}
}