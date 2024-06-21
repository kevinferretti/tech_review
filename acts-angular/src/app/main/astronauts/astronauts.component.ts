import { Component } from '@angular/core';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { DemoMaterialModule } from 'src/app/demo-material-module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { AstronautDutyDto, Client } from 'src/app/api-client/api-client';
import { CommonModule } from '@angular/common';

export enum Rank {
    Specialist1,
    Specialist2,
    Specialist3,
    Specialist4,
    Sergeant,
    TechnicalSergeant,
    MasterSergeant,
    SeniorMasterSergeant,
    ChiefMasterSergeant,
    ChiefMasterSergeantOfTheSpaceForce,
    SecondLieutenant,
    FirstLieutenant,
    Captain,
    Major,
    LieutenantColonel,
    Colonel,
    BrigadierGeneral,
    MajorGeneral,
    LieutenantGeneral,
    General
}

@Component({
	selector: 'astronauts',
	standalone: true,
	imports: [DemoMaterialModule, MatFormFieldModule, MatInputModule, MatTableModule, CommonModule],
	templateUrl: './astronauts.component.html',
	styleUrls: ['./astronauts.component.scss'],
	providers: [
        {
		    provide: STEPPER_GLOBAL_OPTIONS, useValue: { displayDefaultIndicatorType: false }
	    },
        Client
    ]
})
export class AstronautsComponent {
	displayedColumns: string[] = ['startDate', 'endDate', 'title', 'rank'];
	dataSource: AstronautDutyDto[] = [];
	Rank = Rank;

  	constructor(private client: Client) {

  	}

    search(username: string) {
        this.client.astronautDutiesGET(username).subscribe({
            next: (data) => {
                this.dataSource = data.duties || [];
            },
            error: (error) => {
                console.error('Failed to fetch astronaut duties', error);
            }
        });
    }
}