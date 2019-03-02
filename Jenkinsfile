 
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
    steps {
    echo 'Checking   version..'
   }
  }
   
   
   stage('Checkout') {
      steps {
       // Get some code from a GitHub repository
       //  git clone 'https://github.com/manjushavnair/CICDSolution.git'
         checkout scm
         
         }
         
    }
    
     stage('Preparation') { // for display purposes
       
            steps {
              echo 'Checking   version..'
              echo 'node -v'
           echo 'Restore nugets..'
              bat ".\\.nuget\\nuget.exe  restore MVCApp//MVCApp.sln"
            } 
         
   }
  
   stage('Build') {
    steps {
      echo 'Building..'
     
      	bat "\"${tool 'MSBUILD'}\"\\MSBuild.exe ./MVCApp/MVCApp.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

      }
     }
  
  // stage('Results') {
  //   steps {
        //junit '**/target/surefire-reports/TEST-*.xml'
       // archive 'target/*.jar'
  //      }
  // }
   
//   stage ('Notification') {
//    steps {
//       mail from: "jenkins@mycompany.com",
//            to: "devopsteam@mycompany.com",
//            subject: "  build complete",
//            body: "Jenkins job ${env.JOB_NAME} - build ${env.BUILD_NUMBER} complete"
//            }
//  }
   
 }
}

//6. post actions for success or failure of job. Commented out in the following code: Example on how to add a node where a stage is specifically executed. Also, PublishHTML is also a good plugin to expose Cucumber reports but we are using a plugin using Json.
post {
	success {
	//node('node1'){
		echo "Test succeeded"
		script {
		    // configured from using gmail smtp Manage Jenkins-> Configure System -> Email Notification
		    // SMTP server: smtp.gmail.com
		    // Advanced: Gmail user and pass, use SSL and SMTP Port 465
		    // Capitalized variables are Jenkins variables – see https://wiki.jenkins.io/display/JENKINS/Building+a+software+project
		mail(bcc: '',
		     body: "Run ${JOB_NAME}-#${BUILD_NUMBER} succeeded. To get more details, visit the build results page: ${BUILD_URL}.",
		     cc: '',
		     from: 'jenkins-admin@gmail.com',
		     replyTo: '',
		     subject: "${JOB_NAME} ${BUILD_NUMBER} succeeded",
		     to: env.notification_email)
 	    }
	//}
	}
	failure {
		echo "Test failed"
		mail(bcc: '',
		body: "Run ${JOB_NAME}-#${BUILD_NUMBER} succeeded. To get more details, visit the build results page: ${BUILD_URL}.",
		cc:,
		from: 'jenkins-admin@gmail.com',
		replyTo: '',
		subject: "${JOB_NAME} ${BUILD_NUMBER} failed",
		to: env.notification_email)
 	}
}
