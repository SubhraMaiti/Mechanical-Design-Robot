Imports System.Reflection

Public MustInherit Class Part
    Protected _input_parameters As Dictionary(Of String, Object)

    Protected Sub extractInputs()
        Dim fields As FieldInfo() = Me.GetType().GetRuntimeFields

        For Each field As FieldInfo In fields
            If field.FieldType = GetType(DesignInput) Then
                findInputParameterAndSetValue(field)
            End If
        Next

    End Sub

    Protected MustOverride Sub calculateProperties()

    Public Function generateOutput()
        Dim fields As FieldInfo() = Me.GetType().GetRuntimeFields
        For Each field As FieldInfo In fields
            If field.FieldType = GetType(DesignOutput) Then
                addToInputParameters(field)
            End If
        Next

        Return _input_parameters
    End Function

    Private Sub findInputParameterAndSetValue(field As FieldInfo)
        Dim _list_of_input_parameters_name As New List(Of String)(_input_parameters.Keys)

        For Each _input_parameter_name As String In _list_of_input_parameters_name
            If _input_parameter_name = field.Name Then
                field.SetValue(Me, New DesignInput(_input_parameters(_input_parameter_name)))
            End If
        Next
    End Sub

    Private Sub addToInputParameters(field As FieldInfo)
        _input_parameters.Add(field.Name, field.GetValue(Me).value)
    End Sub
End Class


