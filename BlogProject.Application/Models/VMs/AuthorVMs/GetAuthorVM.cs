﻿namespace BlogProject.Application.Models.VMs.AuthorVMs
{
    public class GetAuthorVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
