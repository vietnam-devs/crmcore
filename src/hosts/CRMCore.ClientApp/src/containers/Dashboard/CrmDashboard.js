import React, { Component } from 'react';
import { Card, CardBody } from 'reactstrap';

export default class CrmDashboard extends Component {
  render() {
    return (
      <div className="animated fadeIn">
        <Card className="text-white bg-primary">
          <CardBody>
            <p>Crm Dashboard.</p>
          </CardBody>
          <div style={{ height: '70px' }} />
        </Card>
      </div>
    );
  }
}
