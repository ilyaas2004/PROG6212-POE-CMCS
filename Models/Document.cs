namespace PROG6212_POE_CMCS.Models
{
    public class Document
    { 
  public int DocumentID { get; set; }
    public int ClaimID { get; set; }  // Foreign Key to Claim
    public Claim Claim { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}}