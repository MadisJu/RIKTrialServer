using RIKTrialServer.Domains.Models;
using RIKTrialServer.Repositories.Interfaces;
using RIKTrialServer.Services.Interfaces;
using RIKTrialServer.Transformers;
using RIKTrialSharedModels.Domain.Creation;
using RIKTrialSharedModels.Domain.Filters;
using RIKTrialSharedModels.Domain.Returns;
using RIKTrialSharedModels.Domain.Types;
using RIKTrialSharedModels.Domain.Updates;
using System;

namespace RIKTrialServer.Services.Implementations
{
    public class ParticipantService(IParticipantRepository participantRepo) : IParticipantService
    {
        private readonly IParticipantRepository _participantRepo = participantRepo;

        public async Task<Guid?> CreateParticipant(ParticipantCreationDTO data, CancellationToken ctoken)
        {
            Participant p;
            Guid id = Guid.NewGuid();

            switch (data.Type)
            {
                case ParticipantType.PERSON:

                    if (data.FirstName == null) return null;
                    if (data.LastName == null) return null;
                    if (data.IdNumber == null) return null;

                    p = new Person
                        (
                        id,
                        data.PaymentMethodId,
                        data.FirstName,
                        data.LastName,
                        data.IdNumber,
                        data.AdditionalInfo
                        );
                    break;

                case ParticipantType.COMPANY:

                    int participantAmount = data.ParticipantAmount != null ? data.ParticipantAmount.Value : 0;

                    if (data.Name == null) return null;
                    if (data.ComapnyCode == null) return null;

                    p = new Company
                        (
                        id,
                        data.PaymentMethodId,
                        data.Name,
                        data.ComapnyCode,
                        participantAmount,
                        data.AdditionalInfo
                        );
                    break;

                default:
                    return null;
            }

            if (!(await _participantRepo.AddParticipant(p, ctoken))) return null;

            return id;

        }

        public async Task<bool> DeleteParticipant(Guid id, CancellationToken ctoken)
        {
            Participant? p = await _participantRepo.GetParticipant(id, ctoken);

            if (p == null) return false;

            return await _participantRepo.RemoveAsync(p, ctoken);
        }

        public async Task<List<ParticipantLightReturnDTO>> GetEventParticipants(ParticipantFilters filters, Guid eventId, CancellationToken ctoken)
        {
            List<Participant> participants = await _participantRepo.GetEventParticipants(filters, eventId, ctoken);

            return participants.Select(p => ParticipantMapper.MapToParticipantLightReturnDTO(p)).ToList();
        }

        public async Task<ParticipantReturnDTO> GetParticipant(Guid id, CancellationToken ctoken)
        {
            Participant? p = await _participantRepo.GetParticipant(id, ctoken) ?? throw new Exception("Ei ole sellist osalejat");

            return ParticipantMapper.MapToParticipantReturnDTO(p);
        }

        public async Task<List<ParticipantReturnDTO>> GetParticipants(ParticipantFilters filters, CancellationToken ctoken)
        {
            List<Participant> participants = await _participantRepo.GetParticipants(filters, ctoken);
            return participants.Select(p => ParticipantMapper.MapToParticipantReturnDTO(p)).ToList();
        }

        public async Task<bool> UpdateParticipant(ParticipantUpdateDTO data, Guid id, CancellationToken ctoken)
        {
            Participant? p = await _participantRepo.GetParticipant(id, ctoken);

            if (p == null) return false;

            switch(p)
            {
                case Person person:
                    if (data.Type != ParticipantType.PERSON) return false;
                    if (data.FirstName is string firstname) person.FirstName = firstname;
                    if (data.LastName is string lastName) person.LastName = lastName;
                    //if (data.IdNumber is string idNumber) person.IdNumber = idNumber;
                    if (data.AdditionalInfo is string additionalInfo) person.AdditionalInfo = additionalInfo;
                    if (data.PaymentMethodId is int paymentMethodID) person.PaymentMethodId = paymentMethodID;
                    break;

                case Company comp:
                    if (data.Type != ParticipantType.COMPANY) return false;
                    if (data.Name is string name) comp.Name = name;
                    if (data.CompanyCode is  string comapnyCode) comp.CompanyCode = comapnyCode;
                    if (data.ParticipantAmount is int participantAmount) comp.ParticipantAmount = participantAmount;
                    if (data.AdditionalInfo is string addInfo) comp.AdditionalInfo = addInfo;
                    if (data.PaymentMethodId is int paymentID) comp.PaymentMethodId = paymentID;
                    break;

                default:
                    return false;
            }

            return await _participantRepo.UpdateParticipant(p, ctoken);

        }
    }
}
