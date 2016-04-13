using System.Web.Mvc;
using Protection.Methods;
using Web.User.Models;

namespace Web.Controllers
{
    public class ProtectionController : Controller
    {
        [HttpGet]
        public ActionResult KeySingleProtection()
        {
            var key = new KeySinglePermutationModel.KeyModel()
            {
                RowCount = 3,
                KeyWord = "ПАМИР"
            };
            return View(new KeySinglePermutationModel()
            {
                Input = new KeySinglePermutationModel.InputModel() { Key = key, Value = "ВРАГБУДЕТРАЗБИТ" },
                Output = new KeySinglePermutationModel.OutputModel() { Key = new KeySinglePermutationModel.KeyModel() }
            });
        }

        [HttpPost]
        public ActionResult EncryptKeySingleProtection(KeySinglePermutationModel.InputModel inputModel)
        {
            var method = new KeySinglePermutation();
            var key = new KeySinglePermutation.Key(inputModel.Key.RowCount, inputModel.Key.KeyWord);
            var input = new KeySinglePermutation.Input(inputModel.Value, key);
            var output = method.Encrypt(input);

            var result = new KeySinglePermutationModel()
            {
                Input = inputModel,
                Output = new KeySinglePermutationModel.OutputModel()
                {
                    Key = inputModel.Key,
                    Value = output.Value
                }
            };

            return View("KeySingleProtection", result);
        }

        [HttpPost]
        public ActionResult DecryptKeySinglePermutation(KeySinglePermutationModel.OutputModel outputModel)
        {
            var method = new KeySinglePermutation();
            var key = new KeySinglePermutation.Key(outputModel.Key.RowCount, outputModel.Key.KeyWord);
            var output = new KeySinglePermutation.Output(outputModel.Value, key);
            var input = method.Decrypt(output);

            var result = new KeySinglePermutationModel()
            {
                Input = new KeySinglePermutationModel.InputModel()
                {
                    Key = outputModel.Key,
                    Value = input.Value
                },
                Output = outputModel
            };

            return View("KeySingleProtection", result);
        }
    }
}
