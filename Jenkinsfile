pipeline {
    agent any
    options {
        buildDiscarder(logRotator(daysToKeepStr: '3', numToKeepStr: '10'))
    }
    parameters {
        string(name: 'BRANCH_TO_DEPLOY', defaultValue: '', description: 'Branch to deploy (leave blank for default)')
        booleanParam(name: 'DEPLOY_TEST', defaultValue: false, description: 'Trigger Test Deployment?')
        booleanParam(name: 'DEPLOY_PROD', defaultValue: false, description: 'Trigger Production Deployment?')
    }
    environment {
        DO_HOST      = credentials('DO_HOST')
        DO_USER      = credentials('DO_USER')
        DO_SSH_KEY_ID = 'DO_SSH_KEY'
        PATH         = "$HOME/.dotnet:$PATH"
    }
    stages {
        stage('Checkout') {
            steps {
                script {
                    if (params.BRANCH_TO_DEPLOY?.trim()) {
                        checkout([
                            $class: 'GitSCM',
                            branches: [[name: params.BRANCH_TO_DEPLOY]],
                            userRemoteConfigs: [[
                                url: 'https://github.com/b2atech/dhanman-community.git',
                                credentialsId: env.DO_USER
                            ]]
                        ])
                        echo "Checked out branch: ${params.BRANCH_TO_DEPLOY}"
                    } else {
                        checkout scm
                        echo "Checked out default branch"
                    }
                }
            }
        }
        stage('Set up .NET') {
            steps {
                sh '''
                    wget https://dot.net/v1/dotnet-install.sh
                    chmod +x dotnet-install.sh
                    ./dotnet-install.sh --channel 9.0
                '''
            }
        }
        stage('Gatekeeper Permission Check (Warning Only)') {
            steps {
                sh '''
                    echo "🔐 Running Gatekeeper permission check..."
                    dotnet tool install --global B2A.Gatekeeper.Cli --prerelease
                    OUTPUT=$(b2a-gatekeeper src/Dhanman.MyHome.Api/ 2>&1 || true)
                    echo "$OUTPUT"
                    if [[ "$OUTPUT" == *"Missing [RequiresPermissions]"* ]]; then
                        echo "##[warning] Gatekeeper Warning: Some public controller actions are missing [RequiresPermissions]. See logs above."
                    else
                        echo "✅ Gatekeeper passed with no permission violations."
                    fi
                '''
            }
        }
        stage('Deploy to QA') {
            when {
                anyOf {
                    branch 'main'
                    branch 'develop'
                }
            }
            environment {
                DOTNET_ENVIRONMENT = 'qa'
                COMMUNITYDB   = credentials('CONNECTIONSTRINGS__COMMUNITYDB_QA')
                PERMISSIONSDB = credentials('CONNECTIONSTRINGS__PERMISSIONSDB_QA')
                TEMPLATESDB   = credentials('CONNECTIONSTRINGS__TEMPLATESDB_QA')
            }
            steps {
                sh '''
                    export DOTNET_ENVIRONMENT=${DOTNET_ENVIRONMENT}
                    export ConnectionStrings__CommunityDb="${COMMUNITYDB}"
                    export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}"
                    export ConnectionStrings__TemplatesDb="${TEMPLATESDB}"
                    dotnet publish src/Dhanman.MyHome.Api/Dhanman.MyHome.Api.csproj -c Release -r linux-x64 --self-contained false -o publish
                '''
                sshagent (credentials: [env.DO_SSH_KEY_ID]) {
                    sh '''
                        scp -r publish/* ${DO_USER}@${DO_HOST}:/var/www/qa/dhanman-community
                        ssh ${DO_USER}@${DO_HOST} '
                          export ConnectionStrings__CommunityDb="${COMMUNITYDB}";
                          export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}";
                          export ConnectionStrings__TemplatesDb="${TEMPLATESDB}";
                          export DOTNET_ENVIRONMENT=qa;
                          sudo systemctl restart dhanman-community-qa;
                          sudo systemctl status dhanman-community-qa --no-pager;
                        '
                    '''
                }
            }
        }
        stage('Deploy to Test') {
            when {
                expression { return params.DEPLOY_TEST == true }
            }
            environment {
                DOTNET_ENVIRONMENT = 'test'
                COMMUNITYDB   = credentials('CONNECTIONSTRINGS__COMMUNITYDB_TEST')
                PERMISSIONSDB = credentials('CONNECTIONSTRINGS__PERMISSIONSDB_TEST')
                TEMPLATESDB   = credentials('CONNECTIONSTRINGS__TEMPLATESDB_TEST')
            }
            steps {
                sh '''
                    export DOTNET_ENVIRONMENT=${DOTNET_ENVIRONMENT}
                    export ConnectionStrings__CommunityDb="${COMMUNITYDB}"
                    export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}"
                    export ConnectionStrings__TemplatesDb="${TEMPLATESDB}"
                    dotnet publish src/Dhanman.MyHome.Api/Dhanman.MyHome.Api.csproj -c Release -r linux-x64 --self-contained false -o publish
                '''
                sshagent (credentials: [env.DO_SSH_KEY_ID]) {
                    sh '''
                        scp -r publish/* ${DO_USER}@${DO_HOST}:/var/www/test/dhanman-community
                        ssh ${DO_USER}@${DO_HOST} '
                          export ConnectionStrings__CommunityDb="${COMMUNITYDB}";
                          export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}";
                          export ConnectionStrings__TemplatesDb="${TEMPLATESDB}";
                          export DOTNET_ENVIRONMENT=test;
                          sudo systemctl restart dhanman-community-test;
                          sudo systemctl status dhanman-community-test --no-pager;
                        '
                    '''
                }
            }
        }
        stage('Deploy to Production') {
            when {
                expression { return params.DEPLOY_PROD == true }
            }
            environment {
                DOTNET_ENVIRONMENT = 'production'
                COMMUNITYDB   = credentials('CONNECTIONSTRINGS__COMMUNITYDB_PROD')
                PERMISSIONSDB = credentials('CONNECTIONSTRINGS__PERMISSIONSDB_PROD')
                TEMPLATESDB   = credentials('CONNECTIONSTRINGS__TEMPLATESDB_PROD')
            }
            steps {
                sh '''
                    export DOTNET_ENVIRONMENT=${DOTNET_ENVIRONMENT}
                    export ConnectionStrings__CommunityDb="${COMMUNITYDB}"
                    export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}"
                    export ConnectionStrings__TemplatesDb="${TEMPLATESDB}"
                    dotnet publish src/Dhanman.MyHome.Api/Dhanman.MyHome.Api.csproj -c Release -r linux-x64 --self-contained false -o publish
                '''
                sshagent (credentials: [env.DO_SSH_KEY_ID]) {
                    sh '''
                        scp -r publish/* ${DO_USER}@${DO_HOST}:/var/www/prod/dhanman-community
                        ssh ${DO_USER}@${DO_HOST} '
                          export ConnectionStrings__CommunityDb="${COMMUNITYDB}";
                          export ConnectionStrings__PermissionsDb="${PERMISSIONSDB}";
                          export ConnectionStrings__TemplatesDb="${TEMPLATESDB}";
                          export DOTNET_ENVIRONMENT=production;
                          sudo systemctl restart dhanman-community-prod;
                          sudo systemctl status dhanman-community-prod --no-pager;
                        '
                    '''
                }
            }
        }
    }
    post {
        always {
            cleanWs()
        }
        failure {
            echo 'Deployment failed!'
        }
        success {
            echo 'Deployment succeeded!'
        }
    }
}
