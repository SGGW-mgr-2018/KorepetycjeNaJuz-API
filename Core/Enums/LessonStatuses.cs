namespace KorepetycjeNaJuz.Core.Enums
{
    /// <summary>
    /// Opisuje statusy w których znajduje się lekcja
    /// </summary>
    public enum LessonStatuses
    {
        /// <summary>
        /// Nowo dodana lekcja (coachLesson), która oczekuje na chętnych studentów
        /// Można edytować
        /// Jest widoczna 
        /// </summary>
        WaitingForStudents = 1,

        /// <summary>
        /// Nowo dodane zgłoszenie (lesson), które oczekuje na zaakceptowanie przez korepetytora
        /// Jest widoczne 
        /// </summary>
        WaitingToApprove = 2,

        /// <summary>
        /// Lekcja (coachLesson) do której zgłosiła się co najmniej jedna osoba
        /// Nie można edytować
        /// Nadal jest widoczna i można do niej dołączyć 
        /// </summary>
        Reserved = 3,

        /// <summary>
        /// Lekcja/zgłoszenie (coachLesson/lesson) zatwierdzone/a
        /// Nie można edytować
        /// Nie jest widoczna i nie można do niej dołączyć 
        /// </summary>
        Approved = 4,

        /// <summary>
        /// Lekcja (lesson) odrzucona przez korepetytora
        /// Nie można edytować
        /// </summary>
        Rejected = 5,

        /// <summary>
        /// Zgłoszenie (coachLesson) anulowane przez korepetytora
        /// Nie można edytować
        /// Nie jest widoczna i nie można do niej dołaczyć
        /// </summary>
        Canceled = 6   
    }
}
