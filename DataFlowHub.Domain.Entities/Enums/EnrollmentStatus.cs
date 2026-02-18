namespace DataFlowHub.Domain.Entities.Enums;

public enum EnrollmentStatus
{
    Active,     // El estudiante está cursando actualmente
    Dropped,    // Retiró la materia
    Completed,  // Finalizó el curso (ya tiene nota final)
    Canceled    // La matrícula fue anulada administrativamente
}