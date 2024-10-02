pipeline {
    agent any
    tools {
        // Use the .NET SDK with the configured name
        io.jenkins.plugins.dotnet.DotNetSDK 'dotnet-sdk-6.0'
    }

    environment {
        // SonarQube settings
        SONARQUBE_KEY = 'squ_9d5e5b0c3916633347cdbdc85ec4bb3e54e89c5e'
        PROJECT_NAME = 'projet_pfe'
    }
    
    stages {
        stage('Restore Dependencies') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    sh 'dotnet test'
                }
            }
        }

        stage('Code Analysis with SonarQube') {
            steps {
                script {
                    sh """
                    dotnet sonarscanner begin /k:"${PROJECT_NAME}" /d:sonar.host.url=http://192.168.33.10:9000 /d:sonar.login=${SONARQUBE_KEY}
                    dotnet build
                    dotnet sonarscanner end /d:sonar.login=${SONARQUBE_KEY}
                    """
                }
            }
        }
    }
}
