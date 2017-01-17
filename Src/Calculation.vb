Imports Pot_Bearing_Design_Calculation.DesignOutput
Imports System.Diagnostics

Module Calculation
    Public Function calculate(expression As Double, Optional roundoffagainst As Double = Nothing, Optional minimumvalue As Double = Nothing)

        If (roundoffagainst <> Nothing) Then
            expression = Math.Ceiling(expression / roundoffagainst) * roundoffagainst
        End If

        If (minimumvalue <> Nothing) Then
            If expression < minimumvalue Then
                expression = minimumvalue
            End If
        End If

        Return New DesignOutput(expression)

    End Function

    Public Sub checkSafeOrFail(expression As Double, comparisonoperator As String, comparedValue As Double)
        Dim stackTrace As StackTrace = New StackTrace
        Dim parent_method_name As String = stackTrace.GetFrame(1).GetMethod().Name.ToString()

        If comparisonoperator = "Greater Than" Then
            If expression <= comparedValue Then
                checkSafeOrFailMessage(parent_method_name, "is below permissible limit.")
            End If
        End If

        If comparisonoperator = "Less Than" Then
            If expression >= comparedValue Then
                checkSafeOrFailMessage(parent_method_name, "is above permissible limit.")
            End If
        End If

    End Sub

    Private Sub checkSafeOrFailMessage(methodName As String, message As String)
        methodName = methodName.Replace("check", "")
        Dim messagetoshow As String = ""
        Dim capitalLetterCount As Integer = 0

        For Each c As Char In methodName
            Dim charCode As Integer = AscW(c)
            If charCode >= 65 AndAlso charCode < 91 Then
                capitalLetterCount += 1

                If capitalLetterCount <> 1 Then
                    messagetoshow = messagetoshow + " " + c.ToString.ToLower
                Else
                    messagetoshow = messagetoshow + c.ToString
                End If
            Else
                messagetoshow = messagetoshow + c.ToString
            End If
        Next

        MsgBox(messagetoshow + " " + message)
    End Sub


    Public Sub checkSafeOrAdjust()

    End Sub

End Module
