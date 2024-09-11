' use ALT F11 and add this macro to the excel spread sheet listed below:
' https://h30434.www3.hp.com/t5/Inner-Circle/Let-s-Grow-our-HP-Support-links-library/td-p/9091462
' rename the xls to xlsm before adding this macro.  You will need to unblock the xlsm first.
' jystateson 6/8/2024
' Use notepad to edit HPsupport.html and copy the PC and PRINT portions to the "RF" (referEnces) macro file.
'
Private Function FormHTML(aLine As String, aDoc As String)
    FormHTML = "<a href=""https://support.hp.com/us-en/document/" & aDoc & """" & " target=""_blank"">" & aLine & "</a><br>"
End Function

Sub CopyHPColumnToTextFile()
    Dim ws As Worksheet
    Dim filePath As String
    Dim fileNumber As Integer
    Dim hpURL As String
    Dim i As Long
    Dim lastRow As Long

    ' Set the worksheet and range (adjust the worksheet name and column as needed)
    Set ws = ThisWorkbook.Sheets("PC")
    lastRow = ws.Cells(ws.Rows.Count, "A").End(xlUp).Row

    ' Set the file path for the text file
    filePath = "C:\Users\josep\Downloads\HPsupport.html" ' Change to your desired file path

    ' Get a free file number
    fileNumber = FreeFile

    ' Open the text file for output
    Open filePath For Output As #fileNumber

    For i = 2 To lastRow
        hpURL = FormHTML(ws.Cells(i, 3).Value, ws.Cells(i, 1).Value)
        Print #fileNumber, hpURL
    Next i

    Set ws = ThisWorkbook.Sheets("Print")
    lastRow = ws.Cells(ws.Rows.Count, "A").End(xlUp).Row
    
    
    For i = 2 To lastRow
        hpURL = FormHTML(ws.Cells(i, 3).Value, ws.Cells(i, 1).Value)
        Print #fileNumber, hpURL
    Next i

    ' Close the text file
    Close #fileNumber

    ' Notify the user
    MsgBox "Column copied to text file successfully!"

End Sub

