pipeline {
    agent any
    tools {
        dotnetsdk 'dotnet-sdk-6.0'
    }

    environment {
        SONARQUBE_KEY = 'squ_9d5e5b0c3916633347cdbdc85ec4bb3e54e89c5e'
        PROJECT_NAME = 'projet_pfe'
        SONARQUBE_URL = 'http://192.168.33.10:9000'
        DOTNET_TOOLS_PATH = "$HOME/.dotnet/tools"
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
                        sh 'dotnet build --configuration Release 
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

        stage('Install SonarScanner') {
            steps {
                script {
                    // Install dotnet-sonarscanner if not already installed
                    sh 'dotnet tool install --global dotnet-sonarscanner || true'
                }
            }
        }

        stage('Code Analysis with SonarQube') {
            environment {
                PATH = "${env.DOTNET_TOOLS_PATH}:${env.PATH}"
            }
            steps {
                dir('BackEnd/Gestion User') {
                    script {
                        sh """
                        dotnet sonarscanner begin /k:"${PROJECT_NAME}" /d:sonar.host.url=${SONARQUBE_URL} /d:sonar.login=${SONARQUBE_KEY} \
                        /d:sonar.issuesReport.console.enable=false /d:sonar.issuesReport.html.enable=false \
                        /d:sonar.cs.ignoreIssues=false \
                        /d:sonar.issue.ignore.multicriteria=e1,e2,e3 \
                        /d:sonar.issue.ignore.multicriteria.e1.ruleKey=cs:S2583 \
                        /d:sonar.issue.ignore.multicriteria.e2.ruleKey=cs:S1172 \
                        /d:sonar.issue.ignore.multicriteria.e3.ruleKey=cs:S1166 \
                        /d:sonar.issue.ignore.multicriteria.e1.resourceKey=**/*.cs \
                        /d:sonar.issue.ignore.multicriteria.e2.resourceKey=**/*.cs \
                        /d:sonar.issue.ignore.multicriteria.e3.resourceKey=**/*.cs
                        dotnet build --no-incremental /warnaserror /nowarn:CS8618,CS8603,CS8604,CS8602
                        dotnet sonarscanner end /d:sonar.login=${SONARQUBE_KEY}
                        """
                    }
                }
            }
        }
    }
}
