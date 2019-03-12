
#!groovy
import groovy.json.JsonSlurperClassic
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
		    steps 
			  {
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

def checkoutComponents(components){
    for(def gitUrl : readJsonFromText(components) ) {
        dir(getComponentFolder(gitUrl)) {
            git url: gitUrl
         }
    }
}

def getConfiguration(configurationFileName) {
    def buildConfigurationJsonFile = findFiles(glob: "**/**/$configurationFileName").first()
    readJsonFromFile(buildConfigurationJsonFile.path)
}

def getComponentFolder(giturl) {
    giturl.replace('.git','').tokenize( '/' ).last()
}

def readJsonFromText(def text) { 
    return new JsonSlurperClassic().parseText(text) 
}

def readJsonFromFile(def path) { 
    def configurationFile = new File(env.WORKSPACE, path)
    return new JsonSlurperClassic().parseText(configurationFile.text) 
}

// parse fx cop
def getFxCopReporModel(fxCopReportFileWildCards){
    def reportMap = [:]
    for(def fxCopReportFilePath : getFilePaths(fxCopReportFileWildCards) ) {
        def fxCopReportFile = new File(env.WORKSPACE, fxCopReportFilePath)
        def dllName = fxCopReportFile.name.replace(".fxcop.xml", "");
        def statistic = parseFxCopReportXmlFile(fxCopReportFile)
        echo dllName
        echo statistic
        reportMap.put(dllName, statistic)
    }

    def statisticHtml = '';
    for(def model : reportMap ) {
         statisticHtml+="<li>${model.key}: ${model.value}</li>"
    }
    
    return ["statistic": statisticHtml]
}

def parseFxCopReportXmlFile(fxCopReportFile){
   def errorsCount = 0
   def warningsCount = 0
   def fxCopRootNode = new XmlParser().parse(fxCopReportFile)
   def namespacesNode = getFirstNodeByName(fxCopRootNode.children(), 'Namespaces')
   def namespaceNodes = getAllNodesByName(namespacesNode.children(), 'Namespace');
   
   for(def node : namespaceNodes ) {
       def messagesNode = getFirstNodeByName(node.children(), 'Messages')
       def messageNodes = getAllNodesByName(messagesNode.children(), 'Message')
       for(def messageNode : messageNodes ) {
           def issueNode = getFirstNodeByName(messageNode.children(), 'Issue')
           def issueNodeAttributes = issueNode.attributes()
           def levelAttribute = issueNodeAttributes.get('Level')
           if(levelAttribute != null) {
           if(levelAttribute == 'Warning'){
               warningsCount++
           }
           if(levelAttribute == 'Error'){
               errorsCount++
             }
           }
       }
    }
    
    return "Warnings: ${warningsCount}, Errors: ${errorsCount}"
}

def getFirstNodeByName(nodes, nodeName){
        for(def node : nodes ) {
            if(node.name() == nodeName){
                return node
            }
        }
    }
    
def getAllNodesByName(nodes, nodeName){
        def list = []
        for(def node : nodes ) {
            if(node.name() == nodeName){
               list << node
            }
        }
        return list
    }

def getBuildCompleteModel(nunitResultBody, fxCopResultBody, buildStatus){
    return ["buildResultUrl": "$BUILD_URL", "buildStatus": buildStatus, 
           "buildNumber": "$BUILD_DISPLAY_NAME", "applicationName": "$JOB_NAME",
           "nunitResultBody" : "$nunitResultBody", "fxCopResultBody": "$fxCopResultBody"]
}

def mergeMap(target, map){
    for(def result : map ) { target.put(map.key, map.value) }
    return target
}

def renderTemplete(templateFilePath, model){
    def templateBody =  new File(env.WORKSPACE, templateFilePath).text
    def engine = new groovy.text.SimpleTemplateEngine()
    engine.createTemplate(templateBody).make(model).toString()
}

def getTestReportModel(nunitTestReportXmlFilePath){
    def testXmlRootNode = new XmlParser().parse(new File(env.WORKSPACE, nunitTestReportXmlFilePath))
    def resultNode = findlastNode(testXmlRootNode.children(),'test-suite')
    def result = resultNode.attributes();
    result.put('testResultsUrl', env.JOB_URL + env.BUILD_ID + '/testReport')
    return result
}

def findlastNode(list, nodeName){
    for(def element : list.reverse() ) { 
       if(element.name()==nodeName){
           return element
       }
    }
}

def getFilePaths(wildcards){
    def files = []
    for(def wildcard : wildcards ) { 
        files.addAll(findFiles(glob: wildcard))
    }
    
    def filePaths = []
    for(def file : files ) { filePaths << file.path }
    return filePaths
}

def getFiles(wildcards, rootDir=''){
    def files = []
    for(def wildcard : wildcards ) { 
        files.addAll(findFiles(glob: wildcard))
    }
    
    def names = []
    def prefix = rootDir == '' ? '' : rootDir + '\\'
    for(def file : files ) { names << prefix + file.name }
    return names
}

def cleanDir(dirPath) {
     def dir = new File(dirPath)
     if (dir.exists()) dir.deleteDir()
     if (!dir.exists()) dir.mkdirs()
}

def makeDir(dirPath) {
     def dir = new File(dirPath)
     if (!dir.exists()) dir.mkdirs()
}

def removeDir(dirPath) {
     def dir = new File(dirPath)
     if (dir.exists()) dir.deleteDir()
}
def log(message){
    println message
} 

class BuildStatus {
    static String Ok = 'Ok'
    static String Error = 'Error'
    static String Warning = 'Warning'
}
