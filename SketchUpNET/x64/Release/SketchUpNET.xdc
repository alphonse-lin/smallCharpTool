<?xml version="1.0"?><doc>
<members>
<member name="F:SketchUpNET.Group.Surfaces" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\Group.cpp" line="55">
<summary>
Surfaces
</summary>
</member>
<member name="T:SketchUpNET.SketchUp" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="62">
<summary>
SketchUp Base Class
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Surfaces" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="70">
<summary>
Containing Model Surfaces
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Layers" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="75">
<summary>
Containing Model Layers
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Groups" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="80">
<summary>
Containing Model Groups
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Components" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="85">
<summary>
Containing Model Component Definitions
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Materials" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="90">
<summary>
Containing Model Material Definitions
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Instances" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="95">
<summary>
Containing Model Component Instances
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Curves" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="100">
<summary>
Containing Model Curves (Arcs)
</summary>
</member>
<member name="F:SketchUpNET.SketchUp.Edges" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="105">
<summary>
Containing Model Edges (Lines)
</summary>
</member>
<member name="M:SketchUpNET.SketchUp.LoadModel(System.String)" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="110">
<summary>
Loads a SketchUp Model from filepath without loading Meshes.
Use this if you don't need meshed geometries.
</summary>
<param name="filename">Path to .skp file</param>
</member>
<member name="M:SketchUpNET.SketchUp.LoadModel(System.String,System.Boolean)" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="120">
<summary>
Loads a SketchUp Model from filepath. Optionally load meshed geometries.
</summary>
<param name="filename">Path to .skp file</param>
<param name="includeMeshes">Load model including meshed geometries</param>
</member>
<member name="M:SketchUpNET.SketchUp.SaveAs(System.String,SketchUpNET.SKPVersion,System.String)" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="240">
<summary>
Saves a SketchUp Model from filepath to a new file.
Use this if you want to convert a SketchUp file to a different format.
</summary>
<param name="filename">Path to original .skp file</param>
<param name="version">SketchUp Version to save the new file in</param>
<param name="newFilename">Path to new .skp file</param>
</member>
<member name="M:SketchUpNET.SketchUp.AppendToModel(System.String)" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="297">
<summary>
Append current SketchUp Model Data to an existing SketchUp file.
</summary>
<param name="filename">Path to .skp file</param>
</member>
<member name="M:SketchUpNET.SketchUp.WriteNewModel(System.String)" decl="false" source="C:\Users\CAUPD-BJ141\Downloads\SketchUpNET-master\SketchUpNET-master\SketchUpNET\SketchUpNET.cpp" line="333">
<summary>
Write current SketchUp Model to a new SketchUp file.
</summary>
<param name="filename">Path to .skp file</param>
</member>
</members>
</doc>