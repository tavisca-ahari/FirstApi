pipeline{
    agent any
        parameters{
                string(	name: 'Git_Url',
					defaultValue: "https://github.com/tavisca-ahari/FirstApi.git",
					description: '')

                string(	name: 'Solution_Name',
					defaultValue: "FirstApi.sln", 
					description: '')

                string(	name: 'Image_Name',
					defaultValue: "myfirstapi", 
					description: '')

               /* string(	name: 'Port',
					defaultValue: "5005", 
					description: '')

			   string(	name: 'Contiainer_Port',
					defaultValue: "5005", 
					description: '')*/
			
			string(	name: 'Repository',
					defaultValue: "akshayah08/firstapirepo", 
					description: '')   

             string( name: 'tag_name',
                     defaultValue: "api_tag",
                      description: '')     
            string( name: 'projectKey',
                     defaultValue: "firstapi",
                      description: '')                        
                             

        }

        stages{
            stage('Build'){
                steps{
                     powershell 'dotnet build  ${Solution_Name} -p:Configuration=release -v:q'
                }
            }
            stage('Test'){
                steps{
                    powershell 'dotnet test'
                }
            }
            
         stage('Code Analysis'){
		    steps{
			echo 'SonarQube Code Analysis'
			script{
				withSonarQubeEnv ('SonarQubeServer'){
					withCredentials([usernamePassword(credentialsId: 'Sonarqube_creds', passwordVariable: 'password', usernameVariable: 'username')]){
						powershell 'dotnet ${env:SonarScanner} begin /key:${projectKey} /d:sonar.login="${username}" /d:sonar.password="${password}"'
						powershell 'dotnet build'
						powershell 'dotnet ${env:SonarScanner} end /d:sonar.login="${username}" /d:sonar.password="${password}"'
					}
				}
			}
		}
	}
            stage('Publish'){
                steps{
                     powershell 'dotnet publish -c RELEASE -o Publish'
                }
            }

            stage('Build Docker Image'){
                steps{
                    powershell "docker build -t ${Image_Name} ."
                }
                   
            }

            stage('Tag and Push image to Docker')
            {
                 steps{
                  script{
                    docker.withRegistry('','dockerhub')
                    {
                        
                       powershell "docker tag ${Image_Name} ${Repository}:${tag_name}"
                       powershell "docker push ${Repository}:${tag_name}"
                       powershell "docker image rm -f ${Image_Name}"

                    }
                  }
                    
                }
           }
             
       
             stage('Pull from docker repo')  {
                steps{
                 
                    powershell "docker pull ${Repository}:${tag_name}"

                    }
                }                                
            
            stage('Run Docker image'){
                steps{
			powershell "docker run --rm -p 5005:5005 ${Repository}:${tag_name}"
                }
            }
        }
        post{
            always{
                 deleteDir()
            }
        }    
}

