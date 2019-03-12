 
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
        
   timestamps {
		try
		{
		  stage('Start')
		  {
		    steps 
			  {
		    echo 'Checking   version..'
		    }
		   }
		 } 
	         catch (ex) 
	         {
		    buildStatus = BuildStatus.Error;
		    echo ex
		    exit 1
                 } finally 
		 {
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
   

	 stage('Build SonarQube analysis') 
		 {
			 agent {
		                    label 'master'
		             }
			steps {
				
				
			echo 'SonarQube 1.'
				 
				withSonarQubeEnv('SONARSERVER') 
				{     
				 
					echo "sonar 1 ${tool 'SONARSCANNER'}"
					 
					bat "${tool 'SONARSCANNER'}/bin/sonar-scanner.bat"   

				}
				
				 script 
				  {
				timeout(time: 1, unit: 'HOURS') {
        def qg = waitForQualityGate()
        if (qg.status != 'OK') {
		// error for failing
		// echo for no failure
		
           echo "Pipeline aborted due to quality gate failure: ${qg.status}"
         //   error "Pipeline aborted due to quality gate failure: ${qg.status}"
        }
    }
			}
				
			//	 timeout(time: 1, unit: 'HOURS') {
                   // Parameter indicates whether to set pipeline to UNSTABLE if Quality Gate fails
                    // true = set pipeline to UNSTABLE, false = don't
                    // Requires SonarQube Scanner for Jenkins 2.7+
                    //waitForQualityGate abortPipeline: true
                      //  } 
				// script 
				 //{
				//	 def qualitygate = waitForQualityGate()
				//	 {
				//		if (qualitygate.status != "OK") 
				//		{
				//			error "Pipeline aborted due to quality gate coverage failure: ${qualitygate.status}"
				//		}
				//	 }
				// }
			 }    
		
		 }
  
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

class BuildStatus {
    static String Ok = 'Ok'
    static String Error = 'Error'
    static String Warning = 'Warning'
}
