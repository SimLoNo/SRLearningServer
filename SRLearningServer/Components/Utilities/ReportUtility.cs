using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Interfaces.Utilities;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Utilities
{
    public class ReportUtility : IReportUtility
    {
        IBaseEmailSender _emailSender;
        string _reportEmail;
        public ReportUtility(IBaseEmailSender emailSender, IConfiguration config)
        {
            _emailSender = emailSender;
            _reportEmail = config["DefaultEmailReceiver"];
        }
        public string Format(AttachmentDto entity, string seperator = "<br>")
        {
            string returnString = $"Attachment: {seperator}";
            try
            {
                returnString += "<ul>";
                returnString += $"<li>ID: {entity.AttachmentId} {seperator} </li>";
                returnString += $"<li>Name: {entity.AttachmentName} {seperator}</li>";
                returnString += $"<li>URL: {entity.AttachmentUrl} {seperator}</li>";
                returnString += $"<li>Last Updated: {entity.LastUpdated} {seperator} </li>";
                returnString += $"<li>Active: {entity.Active} {seperator}</li>";
                

                if (!entity.Results.IsNullOrEmpty())
                {
                    returnString += $"<li>Attachment related Results: {seperator}";
                    returnString += "<ul>";
                    returnString += $"{Format(entity.Results, seperator)}";
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                if (!entity.Cards.IsNullOrEmpty())
                {
                    returnString += $"<li>Attachment related Cards: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Cards, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                returnString += "</ul>";
                return returnString;
            }
            catch (Exception ex)
            {
                return $"{returnString} - An error occured win formatting an Attachment for a report, error: {ex.Message}";
            }
        }

        public string Format(IEnumerable<AttachmentDto> entity, string seperator = "<br>")
        {
            string returnString = $"List of Attachments: {seperator}";
            try
            {
                returnString += "<ul>";
                foreach (var attachment in entity)
                {
                    returnString += "<li>";
                    returnString += Format(attachment, seperator);
                    returnString += "</li>";

                }
                returnString += $"</ul> {seperator}";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a list of Attachments for a report, error: {ex.Message}";
            }
            
        }

        public string Format(CardDto entity, string seperator = "<br>")
        {
            string returnString = $"Card: {seperator}";
            try
            {
                returnString += "<ul>";
                returnString += $"<li>ID: {entity.CardId} {seperator}</li>";
                returnString += $"<li>Name: {entity.CardName} {seperator}</li>";
                returnString += $"<li>Attachment ID: {entity.AttachmentId} {seperator}</li>";
                returnString += $"<li>Last Updated: {entity.LastUpdated} {seperator}</li>";
                returnString += $"<li>Active: {entity.Active} {seperator}</li>";
                if (!entity.Types.IsNullOrEmpty())
                {
                    returnString += $"<li>Card related Types: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Types, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                if (!entity.Results.IsNullOrEmpty())
                {
                    returnString += $"<li>Card related Results: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Results, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                if (entity.Attachment != null)
                {
                    returnString += $"<li>Card related Attachment: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Attachment, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                returnString += "</ul>";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a Card for a report, error: {ex.Message}";
            }
            

        }

        public string Format(IEnumerable<CardDto> entity, string seperator = "<br>")
        {
            string returnString = $"List of Cards: {seperator}";
            try
            {
                returnString += "<ul>";
                foreach (var card in entity)
                {
                    returnString += Format(card, seperator);
                }
                returnString += $"</ul> {seperator}";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a list of Cards for a report, error: {ex.Message}";
            }
        }

        public string Format(ResultDto entity, string seperator = "<br>")
        {
            string returnString = $"Result: {seperator}";
            try
            {
                returnString += $"<li>ID: {entity.ResultId} {seperator}</li>";
                returnString += $"<li>Result Name: {entity.ResultText} {seperator}</li>";
                returnString += $"<li>Last Updated: {entity.LastUpdated} {seperator}</li>";
                returnString += $"<li>Active: {entity.Active} {seperator}</li>";
                if (!entity.Cards.IsNullOrEmpty())
                {
                    returnString += $"<li>Result related Cards: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Cards, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                if (entity.Attachment != null)
                {
                    returnString += $"<li>Result related Attachment: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Attachment, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a Result for a report, erro: {ex.Message}";
            }
        }

        public string Format(IEnumerable<ResultDto> entity, string seperator = "<br>")
        {
            string returnString = $"List of Results: {seperator}";
            try
            {

                returnString += "<ul>";
                foreach (var result in entity)
                {
                    returnString += Format(result, seperator);
                }
                returnString += $"</ul> {seperator}";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a list of Results for a report, error: {ex.Message}";
            }
        }

        public string Format(TypeDto entity, string seperator = "<br>")
        {
            string returnString = $"Type: {seperator}";
            try
            {
                returnString += "<ul>";
                returnString += $"<li>ID: {entity.TypeId} {seperator}</li>";
                returnString += $"<li>Type Name: {entity.CardTypeName} {seperator} </li>";
                returnString += $"<li>Last Updated: {entity.LastUpdated} {seperator} </li>";
                returnString += $"<li>Active: {entity.Active} {seperator} </li>";
                if (!entity.Cards.IsNullOrEmpty())
                {
                    returnString += $"<li>Type related Cards: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Cards, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                if (!entity.TypeCategoryLists.IsNullOrEmpty())
                {
                    returnString += $"<li>Type related Type Category Lists: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.TypeCategoryLists, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                returnString += "</ul>";
                return returnString;

            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a Type for a report, error: {ex.Message}";
            }
        }
        public string Format(IEnumerable<TypeDto> entity, string seperator = "<br>")
        {
            string returnString = $"List of Type: {seperator}";
            try
            {
                returnString += "<ul>";
                foreach (var type in entity)
                {
                    returnString += Format(type, seperator);
                }
                returnString += $"</ul> {seperator}";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a Type for a report, error: {ex.Message}";
            }
        }

        public string Format(TypeCategoryListDto entity, string seperator = "<br>")
        {
            string returnString = $"TypeCategoryList: {seperator}";
            try
            {
                returnString += "<ul>";
                returnString += $"<li>ID: {entity.TypeCategoryListId} {seperator} </li>";
                returnString += $"<li>TypeCategoryList Name: {entity.TypeCategoryListName} {seperator} </li>";
                returnString += $"<li>Last Updated: {entity.LastUpdated} {seperator}</li>";
                returnString += $"<li>Active: {entity.Active} {seperator}</li>";
                if (!entity.Types.IsNullOrEmpty())
                {
                    returnString += $"<li>TypeCategoryList related Types: {seperator}";
                    returnString += "<ul>";
                    returnString += Format(entity.Types, seperator);
                    returnString += $"</ul> {seperator}";
                    returnString += "</li>";
                }
                returnString += "</ul>";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a TypeCategoryList for a report, error: {ex.Message}";
            }
        }

        public string Format(IEnumerable<TypeCategoryListDto> entity, string seperator = "<br>")
        {
            string returnString = $"List of TypeCategoryLists: {seperator}";
            try
            {
                returnString += "<ul>";
                foreach (var typeCategoryList in entity)
                {
                    returnString += Format(typeCategoryList, seperator);
                }
                returnString += $"</ul> {seperator}";
                return returnString;
            }
            catch (Exception ex)
            {

                return $"{returnString} - An error occured in formatting a list of TypeCategoryList for a report, error: {ex.Message}";
            }
        }


        public async Task GenerateReport(string message)
        {
            await _emailSender.SendEmailAsync(_reportEmail, $"Fejl Raporteret - {Guid.NewGuid()}", message);
            return;
        }
        public async Task GenerateContact(string message, string topic)
        {
            await _emailSender.SendEmailAsync(_reportEmail, $"Kontakt form: {topic} - {Guid.NewGuid()}", message);
            return;
        }
    }
}
