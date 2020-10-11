import { Component, Input, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetMonthlyPremiumService } from './get-premium.service';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
 
@Component({
  selector: 'app-get-premium',
  templateUrl: './get-premium.component.html',
  encapsulation: ViewEncapsulation.None
})
export class GetPremiumComponent {

  public premium: number;
  public premiumCalculationForm: FormGroup;
  public heroForm: FormGroup;
  public showPremium: boolean = false;
  public message: string;
  public userSpecificInfo: ProspectiveUserInfo;
  @Input() max: any;
  tomorrow = new Date();
  constructor(private apiPremiumService: GetMonthlyPremiumService, private _formBuilder: FormBuilder) {
    this.tomorrow.setDate(this.tomorrow.getDate());
  }

  ngOnInit(): void {
    this.initForm();
  }

  occupations: occupations[] = [
    { text: 'Author', value: Rating.WhiteCollar },
    { text: 'Cleaner', value: Rating.LightManual },
    { text: 'Doctor', value: Rating.Professional },
    { text: 'Farmer', value: Rating.HeavyManual },
    { text: 'Florist', value: Rating.LightManual },
    { text: 'Mechanic', value: Rating.HeavyManual }
  ];


  onOccupationChange() {

    if (!this.premiumCalculationForm.valid) {
      return false;
    }
    console.log('Occupation changed...');
    var userInfo = new ProspectiveUserInfo();
    userInfo.age = parseInt(this.premiumCalculationForm.controls['age'].value);
    userInfo.name = this.premiumCalculationForm.controls['name'].value;
    userInfo.deathsuminsured = parseInt(this.premiumCalculationForm.controls['deathsuminsured'].value);
    userInfo.occupation = this.premiumCalculationForm.controls['occupation'].value;
    userInfo.dateofbirth = this.premiumCalculationForm.controls['dob'].value;
    var monthlyPremiumRate: number = 0;
    this.apiPremiumService.GenerateMonthlyPremiumRate(userInfo).subscribe(data => {
      monthlyPremiumRate = data.premium;
      this.showPremium = true;
      this.premium = monthlyPremiumRate;
      this.message = "Hi <strong>" + userInfo.name + "</strong>, yours monthly premium will be <strong>" + this.premium + "</strong> ÄUD.";
    },
      err => {
        console.log("error occurred" + err);
      });
  }

  initForm() {
    this.premiumCalculationForm = this._formBuilder.group({
      "name": ["", [Validators.required]],
      "age": ["", [Validators.required, Validators.minLength(1), Validators.maxLength(2)]],
      "deathsuminsured": ["", [Validators.required, Validators.minLength(1), Validators.maxLength(7), Validators.min(1)]],
      "occupation": ["", [Validators.required]],
      "dob": ["", [Validators.required]],
    });
  }

  resetControls() {
    this.premiumCalculationForm.clearValidators();
    this.initForm();
  }

  ngOnDestroy() { this.resetControls(); }
}


export class ProspectiveUserInfo {
  dateofbirth: string;
  age: number;
  name: string;
  deathsuminsured: number;
  occupation: string;
}

export enum Rating {
  LightManual,
  HeavyManual,
  Professional,
  WhiteCollar
}

export class occupations {
  text: string;
  value: Rating;
}


