LOCAL CRLF
CRLF = CHR(13) + CHR(10)
Response.Write([<!DOCTYPE html>]+ CRLF +;
   [<html>]+ CRLF +;
   [<head> ]+ CRLF +;
   [		<meta charset="utf-8" /> ]+ CRLF +;
   [	 	<meta http-equiv="X-UA-Compatible" content="IE=edge" />]+ CRLF +;
   [	 <title>])

Response.Write(TRANSFORM( EVALUATE([ TRIM(oHelp.oTopic.Topic) ]) ))

Response.Write([ - ])

Response.Write(TRANSFORM( EVALUATE([ oHelp.cProjectName ]) ))

Response.Write([</title>	 ]+ CRLF +;
   [	 <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1" />]+ CRLF +;
   [	 <!-- <script type="text/javascript" src="https://getfirebug.com/firebug-lite.js#startOpened,overrideConsole"></script> -->]+ CRLF +;
   [	<link rel="stylesheet" type="text/css" href="templates/scripts/bootstrap/dist/css/bootstrap.min.css" />	]+ CRLF +;
   [	<link rel="stylesheet" type="text/css" href="templates/scripts/fontawesome/css/font-awesome.min.css" />]+ CRLF +;
   [	<link rel="stylesheet" type="text/css" href="templates/wwhelp.css" />]+ CRLF +;
   []+ CRLF +;
   [	 	<script src="templates/scripts/jquery/jquery.min.js"></script>	]+ CRLF +;
   []+ CRLF +;
   [		 <script src="templates/scripts/highlightjs/highlight.pack.js"></script>] + CRLF )
Response.Write([	 <link href="templates/scripts/highlightjs/styles/vs2015.css" rel="stylesheet" />]+ CRLF +;
   []+ CRLF +;
   [	<script src="templates/scripts/ww.jquery.min.js"></script>]+ CRLF +;
   [	<script src="templates/scripts/wwhelp.js"></script>]+ CRLF +;
   []+ CRLF +;
   [	<topictype value="])

Response.Write(TRANSFORM( EVALUATE([ TRIM(oHelp.oTopic.Type) ]) ))

Response.Write([" />		  ]+ CRLF +;
   [	<script>		  ]+ CRLF +;
   [	$(document).ready( function() {]+ CRLF +;
   [		helpBuilder.initializeLayout();		  ]+ CRLF +;
   [		// expand all top level topics]+ CRLF +;
   [		setTimeout( helpBuilder.tocExpandTop, 5);]+ CRLF +;
   [	});]+ CRLF +;
   [	</script>]+ CRLF +;
   [</head>]+ CRLF +;
   [<body>] + CRLF )
Response.Write([])

 lcSeeAlsoTopics = oHelp.InsertSeeAlsoTopics() 
Response.Write([]+ CRLF +;
   [	 <div class="flex-master">]+ CRLF +;
   [		  <div class="banner">]+ CRLF +;
   []+ CRLF +;
   [				<div class="pull-right sidebar-toggle">]+ CRLF +;
   [					 <i class="fa fa-bars"]+ CRLF +;
   [						 title="Show or hide the topics list"></i>]+ CRLF +;
   [				</div>]+ CRLF +;
   [				<img src="images/logo.png" class="banner-logo" />]+ CRLF +;
   [] + CRLF )
Response.Write([				<div class="projectname">])

Response.Write(TRANSFORM( EVALUATE([ oHelp.cProjectname ]) ))

Response.Write([</div>]+ CRLF +;
   [				<div class="byline">]+ CRLF +;
   [					 <img src="bmp/])

Response.Write(TRANSFORM( EVALUATE([ TRIM(oHelp.oTopic.Type)]) ))

Response.Write([.png">])

Response.Write(TRANSFORM( EVALUATE([ iif(oHelp.oTopic.Static,[<img src="bmp/static.png" />] + ']' + [,[] + ']' + [) ]) ))

Response.Write([&nbsp;])

Response.Write(TRANSFORM( EVALUATE([ EncodeHtml(TRIM(oHelp.oTopic.Topic)) ]) ))

Response.Write([]+ CRLF +;
   [				</div>]+ CRLF +;
   [		  </div>]+ CRLF +;
   []+ CRLF +;
   [		  <div class="page-content">]+ CRLF +;
   [				<div id="toc" class="sidebar-left toc-content">					 ]+ CRLF +;
   [					 <nav class="visually-hidden">]+ CRLF +;
   [						  <a href="tableofcontents.htm">Table of Contents</a>]+ CRLF +;
   [					 </nav>]+ CRLF +;
   [				</div>] + CRLF )
Response.Write([]+ CRLF +;
   [				<div class="splitter">					 ]+ CRLF +;
   [				</div>]+ CRLF +;
   []+ CRLF +;
   [				<nav class="topic-outline">]+ CRLF +;
   [					 <div class="topic-outline-header">On this page:</div>]+ CRLF +;
   [					 <div class="topic-outline-content"></div>]+ CRLF +;
   [				</nav>]+ CRLF +;
   []+ CRLF +;
   [				<div class="main-content">] + CRLF )
Response.Write([					 <!-- Rendered Content -->]+ CRLF +;
   [					 <article class="content-pane">								]+ CRLF +;
   [])

 wwScript.RenderAspScript(wwScriptContentPage)
Response.Write([]+ CRLF +;
   [					 </article>]+ CRLF +;
   []+ CRLF +;
   []+ CRLF +;
   [					 <hr />]+ CRLF +;
   [					 <div class="pull-right">]+ CRLF +;
   [						  <a href="https://helpbuilder.west-wind.com" target="_blank"><img src="images/wwhelp.png" /></a>]+ CRLF +;
   [					 </div>]+ CRLF +;
   []+ CRLF +;
   [					 <small>] + CRLF )
Response.Write([						  &copy; ])

Response.Write(TRANSFORM( EVALUATE([ oHelp.cProjCompany ]) ))

Response.Write([, ])

Response.Write(TRANSFORM( EVALUATE([ Year(Date()) ]) ))

Response.Write([ &bull;]+ CRLF +;
   [						  Updated: ])

Response.Write(TRANSFORM( EVALUATE([ TTOD(oHelp.oTopic.Updated) ]) ))

Response.Write([]+ CRLF +;
   [						  <br />]+ CRLF +;
   [						  <a href="mailto:support@yoursite.com?subject=])

Response.Write(TRANSFORM( EVALUATE([ TRIM(oHelp.oTopic.Topic) ]) ))

Response.Write([ (])

Response.Write(TRANSFORM( EVALUATE([ oHelp.oTopic.Pk ]) ))

Response.Write([)&body=Topic: ])

Response.Write(TRANSFORM( EVALUATE([ oHelp.cProjectName ]) ))

Response.Write([ - http://yoursite.com/docs/])

Response.Write(TRANSFORM( EVALUATE([ oHelp.oTopic.Pk ]) ))

Response.Write([.htm">Comment or report problem with topic</a>]+ CRLF +;
   [					 </small>]+ CRLF +;
   [					 <br class="clearfix" />]+ CRLF +;
   [					 <br />]+ CRLF +;
   [					 <!-- End Rendered Content -->]+ CRLF +;
   [				</div>]+ CRLF +;
   [		  </div>		  ]+ CRLF +;
   [	 </div>]+ CRLF +;
   [</body>]+ CRLF +;
   [</html>] + CRLF )
Response.Write([])
