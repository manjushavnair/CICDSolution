 
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
        
  
  stage('Start')
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
	 //     checkout([
  //$class: 'GitSCM', branches: [[name: '*/master']],
 // userRemoteConfigs: [[url: 'git@bitbucket.org:BRNTZN/repository2.git',credentialsId:'jenkinsmaster']]
//])
         
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
 echo 'Building completed..'
      }
     }
  
  // stage('Results') {
  //   steps {
        //junit '**/target/surefire-reports/TEST-*.xml'
       // archive 'target/*.jar'
  //      }
  // }
   
//	  stage('Build SonarQube analysis') {
	//  steps {
	 //script {
	 //def sqScannerMsBuildHome = tool 'SONARSCANNER'
	// }
	// withSonarQubeEnv('SONARSERVER') {
	// echo 'sonar 1..${sqScannerMsBuildHome}'
	// Due to SONARMSBRU-307 value of sonar.host.url and credentials should be passed on command line
	//bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe begin /k:HPSPED /n:hpsprojectdigitization /v:1.0 /d:sonar.host.url=https://sonarqube.honeywell.com/ /d:sonar.login=0e417dae7101e1a21eb6170f802fffb9e81d0129"
//	bat "${sqScannerMsBuildHome}/bin/sonar-scanner.bat"   

	// bat 'MSBuild.exe /t:Rebuild'
	//   bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe end"

//	  }
//	 script {
	//	 def qualitygate = waitForQualityGate()
		// if (qualitygate.status != "OK") {
		  //  error "Pipeline aborted due to quality gate coverage failure: ${qualitygate.status}"
		// }
	// }
//	 }    

	// }
  
  
   stage ('Notification') {
    steps {
       mail from: "manjusha.saju@honeywell.com",
            to: "DL_HPS_IPE@HoneywellProd.onmicrosoft.com",
            subject: "  build complete",
            body: "Jenkins job ${env.JOB_NAME} - build ${env.BUILD_NUMBER} complete"
            }
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
		    // Capitalized variables are Jenkins variables  see https://wiki.jenkins.io/display/JENKINS/Building+a+software+project
		mail(bcc: '',
		     body: "Run ${JOB_NAME}-#${BUILD_NUMBER} succeeded. To get more details, visit the build results page: ${BUILD_URL}.",
		     cc: '',
		     from: 'manjusha.saju@honeywell.com',
		     replyTo: 'manjusha.saju@honeywell.com',
		     subject: "${JOB_NAME} ${BUILD_NUMBER} succeeded",
		     to: "DL_HPS_IPE@HoneywellProd.onmicrosoft.com")
 	    }
	//}
	}
	failure {
		echo "Test failed"
		mail(bcc: '',
		body: "Run ${JOB_NAME}-#${BUILD_NUMBER} succeeded. To get more details, visit the build results page: ${BUILD_URL}.",
		cc:'',
		from: 'manjusha.saju@honeywell.com',
		replyTo: '',
		subject: "${JOB_NAME} ${BUILD_NUMBER} failed",
		to: env.notification_email)
 	}
	unstable {
       echo 'Pipeline run marked unstable'

        }
}
}
