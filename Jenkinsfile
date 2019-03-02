 
pipeline {

agent any

parameters {
	// 2.variables for the parametrized execution of the test: Text and options
	choice(choices: 'yes\nno', description: 'Are you sure you want to execute this test?', name: 'run_test_only')
	choice(choices: 'yes\nno', description: 'Archived war?', name: 'archive_war')
	string(defaultValue: "your.email@gmail.com", description: 'email for notifications', name: 'notification_email')
}
//3. Environment variables
environment {
	firstEnvVar= 'FIRST_VAR'
	secondEnvVar= 'SECOND_VAR'
	thirdEnvVar= 'THIRD_VAR'
}

stages {
        
  
  stage('first')
  {
   def mvnHome
   }
   
   stage('Configure') {
    env.PATH = "${tool 'MSBuild'}/bin:${env.PATH}"
     }
  
    stage('Checkout') {
   
    // Get some code from a GitHub repository
      git 'https://github.com/manjushavnair/CICDSolution.git'
      mvnHome = tool 'M3'
      
    }
    
   stage('Preparation') { // for display purposes
   
        steps {
        bat echo 'Checking   version..'
        bat echo 'node -v'
        bat echo 'Restore nugets..'
        bat 'nuget restore MVCApp.sln'
        } 
     
   }
   stage('Build') {
    bat echo 'Building..'
    bat "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\msbuild.exe" MVCApp//MVCApp.sln /noautorsp /ds /nologo /t:clean,rebuild /p:Configuration=Debug /v:m /p:VisualStudioVersion=14.0

     }
   stage('Results') {
      junit '**/target/surefire-reports/TEST-*.xml'
      archive 'target/*.jar'
   }
   
   stage ('Notification') {
    mail from: "jenkins@mycompany.com",
         to: "devopsteam@mycompany.com",
         subject: "Terraform build complete",
         body: "Jenkins job ${env.JOB_NAME} - build ${env.BUILD_NUMBER} complete"
  }
 
} 
}