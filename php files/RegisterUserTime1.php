<?php
//$servername = "localhost"; //use url if server is online
$servername = "database-aws.cimfp3oazumo.us-east-2.rds.amazonaws.com"; //use url if server is online
$username = "admin";
$password = "human-robot";
$dbname = "usersDB";

//variables submitted by user

$loginUser = $_POST["loginUser"];
$loginTime1 = $_POST["loginTime1"];
// $loginTime1 = 6;

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// echo "Connected Successfully!"

$sql = "UPDATE `usersDB`.`users` SET `totaltime_1` = ('" . $loginTime1 . "') WHERE (`username` = '" . $loginUser . "' )";

$result = $conn->query($sql);
echo $result;

$conn->close();
?>