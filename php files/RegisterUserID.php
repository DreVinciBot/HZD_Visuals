<?php
//$servername = "localhost"; //use url if server is online
$servername = "database-aws.cimfp3oazumo.us-east-2.rds.amazonaws.com"; //use url if server is online
$username = "admin";

$dbname = "usersDB";


//variables submitted by user
$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];



// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// echo "Connected Successfully!"

$sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'"; 
$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    // Tell user that name is alrady taken
    echo "Username is already taken.";
} 
else 
{
    //Insert the user and password into the database
    $sql_new = "INSERT INTO users (username) VALUES ('" . $loginUser . "')";
    if ($conn->query($sql_new) === TRUE) 
    {
        echo "created user";
    } 
    else 
    {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }      
}
$conn->close();
?>